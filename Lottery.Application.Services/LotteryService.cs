using System.Text.Json;

using Lottery.Application.Services.Resolvers;
using Lottery.Domain.Abstract.Application;
using Lottery.Domain.Abstract.Infrastructure.Database;
using Lottery.Domain.Models.Common;
using Lottery.Domain.Models.Database.Entities;
using Lottery.Domain.Models.Dto;
using Lottery.Domain.Models.Request;
using Lottery.Domain.Specifications.Draw;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Lottery.Application.Services
{
    public class LotteryService : ILotteryService
    {
        private readonly INumberService _generatorService;
        private readonly IDrawHistoryRepository _repository;
        private readonly IMemoryCache _cache;
        private readonly ILogger<LotteryService> _logger;
        private readonly GenerationSideStateManager _generationSideStateManager;

        private const string CacheKey = "LastGeneratedNumbers";

        public LotteryService(
            INumberService generatorService,
            IDrawHistoryRepository repository,
            IMemoryCache cache,
            ILogger<LotteryService> logger,
            GenerationSideStateManager generationSideStateManager)
        {
            _generatorService = generatorService ?? throw new ArgumentNullException(nameof(generatorService));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _generationSideStateManager = generationSideStateManager;
        }

        public int[] GenerateDraw( )
        {
            //Log messages should be ideally stored in separate file as constants, or even better provided with logging middleware.
            //In this application, there's only one service with logging and nearly all log msgs are unique, thus not implemented
            _logger.LogInformation("Generating lottery numbers...");
            try
            {
                //Automatically switch to SSG when generate is called
                if ( !_generationSideStateManager.ServerSide ) _generationSideStateManager.Set(true);
                var numbers = _generatorService.Generate();
                _logger.LogInformation("Successfully generated numbers: {Numbers}", string.Join(", ", numbers));
                _cache.Set(CacheKey, numbers, TimeSpan.FromMinutes(10));
                _logger.LogInformation("Generated numbers cached: {Numbers}", string.Join(", ", numbers));
                //In case of bigger application or data manipulation / shielding client from DB structure, DTOs should also be defined
                //Personally, I'd use AutoMapper in this case.
                //Using entities app-wide, due to the size and needs of this specific app
                return numbers;
            }
            catch ( Exception ex )
            {
                _logger.LogError(ex, "Error occurred while generating lottery numbers.");
                throw;
            }
        }

        /// <summary>
        /// Clears cached numbers in case of user switching to client-side numbers generation and informs server about generation mode
        /// </summary>
        /// <remarks>
        /// Application uses both client-side and server-side number generation, which user can switch.
        /// If the uses server-side generation, we can store what we sent to user in cache.
        /// If the user then for example catches the Save POST request e.g. in Burp and rewrites the values,
        /// we can determine whether the user "cheated".
        /// Based on this, further actions may be initiated.
        /// </remarks>
        public void SetGenerationMode(bool isServerSide)
        {
            _generationSideStateManager.Set(isServerSide);

            if ( !isServerSide )
            {
                if ( _cache.TryGetValue(CacheKey, out _) )
                {
                    _cache.Remove(CacheKey);
                    _logger.LogInformation("Cache cleared: Client-side generation enabled.");
                }
            }

            var mode = isServerSide ? "Server-side" : "Client-side";
            _logger.LogInformation("{Mode} generation mode enabled.", mode);
        }

        public bool IsServerSideGeneration( )
        {
            return _generationSideStateManager.ServerSide;
        }

        public async Task SaveDrawAsync(int[] numbers)
        {
            _logger.LogInformation("Attempting to save draw: {Numbers}", string.Join(", ", numbers ?? Array.Empty<int>( )));

            if ( numbers == null || !numbers.Any( ) )
            {
                var message = "Numbers cannot be null or empty.";
                _logger.LogWarning(message);
                throw new ArgumentException(message, nameof(numbers));
            }

            CheckCache(numbers);

            var drawLog = new DrawLog
            {
                //Created could be also set during context SaveChanges() automatically
                Created = DateTime.UtcNow,
                DrawNumbers = numbers.Select((number, index) => new DrawNumber
                {
                    Number = number,
                    Index = index
                }).ToList()
            };

            try
            {
                await _repository.AddAsync(drawLog);
                _logger.LogInformation("Draw saved successfully with ID: {Id}", drawLog.Id);
            }
            catch ( Exception ex )
            {
                _logger.LogError(ex, "Error occurred while saving draw to the database.");
                throw;
            }
        }

        public async Task<PagedResult<DrawLogDto>> GetDrawHistoryAsync(DrawHistoryRequest request)
        {
            _logger.LogInformation("Fetching draw history...");
            try
            {
                var criteria = DrawLogFilterResolver.CreateCriteria(request);

                var history = await _repository.GetPagedAsync(new DrawHistoryByFilterCriteriaSpecification(criteria, request), request.PageIndex, request.PageSize);
                _logger.LogInformation("Successfully retrieved {Count} draw entries.", history.Items.Count);
                var result = MapToDto(history);
                return result;
            }
            catch ( Exception ex )
            {
                _logger.LogError(ex, "Error occurred while fetching draw history.");
                throw;
            }
        }

        private PagedResult<DrawLogDto> MapToDto(PagedResult<DrawLog> history)
        {
            //In case of larger entities, I'd personally go for Automapper
            return new PagedResult<DrawLogDto>(
                history.Items.Select(x => new DrawLogDto( )
                {
                    Id = x.Id,
                    Created = x.Created,
                    Numbers = x.DrawNumbers.Select(y => new DrawNumberDto( )
                    {
                        Number = y.Number,
                        Index = y.Index
                    }).OrderBy(y => y.Index)
                }).ToList( ),
          history.TotalCount,
           history.PageIndex,
            history.PageSize
                );
        }

        private void CheckCache(int[] numbers)
        {
            if ( _generationSideStateManager.ServerSide && _cache.TryGetValue(CacheKey, out int[]? cachedNumbers) )
            {
                if ( cachedNumbers is null )
                {
                    _logger.LogInformation("Server cache is empty");
                }
                else if ( !numbers.SequenceEqual(cachedNumbers) )
                {
                    _logger.LogWarning("Numbers submitted do not match cached numbers. Expected {Expected}, got {Actual}.",
                        string.Join(", ", cachedNumbers), string.Join(", ", numbers));
                }
            }
        }
    }
}
