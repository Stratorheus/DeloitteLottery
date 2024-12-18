using Lottery.Domain.Abstract.Application;
using Lottery.Domain.Models.Common;
using Lottery.Domain.Models.Constants.OrderableProperties;
using Lottery.Domain.Models.Database.Entities;
using Lottery.Domain.Models.Dto;
using Lottery.Domain.Models.Request;

using Microsoft.AspNetCore.Mvc;

namespace Lottery.Presentation.Server.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrawController : ControllerBase
    {
        private readonly ILotteryService _lotteryService;
        private readonly ILogger<DrawController> _logger;

        public DrawController(ILotteryService lotteryService, ILogger<DrawController> logger)
        {
            _lotteryService = lotteryService;
            _logger = logger;
        }

        [HttpGet("generate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int[]))]
        public IActionResult Generate( )
        {
            var numbers = _lotteryService.GenerateDraw();
            return Ok(numbers);
        }

        [HttpPost("generation-mode")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof((bool, string)))]
        public IActionResult SetGenerationMode([FromBody] bool isServerSide)
        {
            _lotteryService.SetGenerationMode(isServerSide);
            var mode = isServerSide ? "Server-side" : "Client-side";
            return Ok(new { IsServerSide = isServerSide, Message = $"{mode} generation mode enabled." });
        }

        [HttpGet("generation-mode")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof((bool, string)))]
        public IActionResult GetGenerationMode( )
        {
            var isServerSide = _lotteryService.IsServerSideGeneration();
            var mode = isServerSide ? "Server-side" : "Client-side";
            return Ok(new { IsServerSide = isServerSide, Message = $"{mode} generation mode is currently active." });
        }

        [HttpPost("save")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Save([FromBody] int[] numbers)
        {
            try
            {
                _logger.LogInformation("Attempting to save draw...");
                await _lotteryService.SaveDrawAsync(numbers);
                return Ok(new { Message = "Draw saved successfully" });
            }
            catch ( ArgumentException ex )
            {
                _logger.LogWarning(ex, "Validation error occurred.");
                return BadRequest(new { ex.Message });
            }
            catch ( Exception ex )
            {
                _logger.LogError(ex, "Unexpected error while saving draw.");
                throw;
            }
        }

        //Could be also done with healthcheck, with this approach we can check specific controller
        [HttpGet("ping")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Ping( )
        {
            return Ok( );
        }

        [HttpPost("history")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<DrawLogDto>))]
        public async Task<IActionResult> GetHistory([FromBody] DrawHistoryRequest request)
        {
            var history = await _lotteryService.GetDrawHistoryAsync(request);
            return Ok(history);
        }

        [HttpGet("orderable-fields")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<string>))]
        public IActionResult GetOrderableField( )
        {
            var fields = DrawLogOrderableProperties.GetAll();
            return Ok(fields);
        }
    }
}
