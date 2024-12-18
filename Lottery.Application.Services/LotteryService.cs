using System.Text.Json;

using Lottery.Domain.Abstract.Application;
using Lottery.Domain.Abstract.Infrastructure.Database;
using Lottery.Domain.Models.Database.Entities;

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

        private const string CacheKey = "LastGeneratedNumbers";
        private bool _serverSideGeneration = false;

        public LotteryService(
            INumberService generatorService,
            IDrawHistoryRepository repository,
            IMemoryCache cache,
            ILogger<LotteryService> logger)
        {
            _generatorService = generatorService ?? throw new ArgumentNullException(nameof(generatorService));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public int[] GenerateDraw( )
        {
            //Log messages should be ideally stored in separate file as constants, or even better provided with logging middleware.
            //In this application, there's only one service with logging and nearly all log msgs are unique, thus not implemented
            _logger.LogInformation("Generating lottery numbers...");
            try
            {
                var numbers = _generatorService.Generate();
                _logger.LogInformation("Successfully generated numbers: {Numbers}", string.Join(", ", numbers));
                _cache.Set(CacheKey, numbers, TimeSpan.FromMinutes(10));
                _logger.LogInformation("Generated numbers cached: {Numbers}", string.Join(", ", numbers));
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
            _serverSideGeneration = isServerSide;

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

        public async Task SaveDrawAsync(int[] numbers)
        {
            _logger.LogInformation("Attempting to save draw: {Numbers}", string.Join(", ", numbers ?? Array.Empty<int>( )));

            if ( numbers == null || !numbers.Any( ) )
            {
                var message = "Numbers cannot be null or empty.";
                _logger.LogWarning(message);
                throw new ArgumentException(message, nameof(numbers));
            }

            if ( _serverSideGeneration && _cache.TryGetValue(CacheKey, out int[]? cachedNumbers) )
            {
                if (cachedNumbers is null )
                {
                    _logger.LogInformation("Server cache is empty");
                }
                else if ( !numbers.SequenceEqual(cachedNumbers) )
                {
                    _logger.LogWarning("Numbers submitted do not match cached numbers. Expected {Expected}, got {Actual}.",
                        string.Join(", ", cachedNumbers), string.Join(", ", numbers));
                }
            }

            var drawLog = new DrawLog
            {
                //Could be also set during context SaveChanges() automatically
                Created = DateTime.UtcNow,
                Numbers = JsonSerializer.Serialize(numbers)
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

        public async Task<IReadOnlyList<DrawLog>> GetDrawHistoryAsync( )
        {
            _logger.LogInformation("Fetching draw history...");
            try
            {
                var history = await _repository.GetAllAsync();
                _logger.LogInformation("Successfully retrieved {Count} draw entries.", history.Count);
                return history;
            }
            catch ( Exception ex )
            {
                _logger.LogError(ex, "Error occurred while fetching draw history.");
                throw;
            }
        }
    }
}
