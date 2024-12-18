using Lottery.Domain.Abstract.Application;
using Lottery.Domain.Models.Database.Entities;

using Microsoft.AspNetCore.Mvc;

namespace Lottery.Presentation.Server.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LotteryController : ControllerBase
    {
        private readonly ILotteryService _lotteryService;
        private readonly ILogger<LotteryController> _logger;

        public LotteryController(ILotteryService lotteryService, ILogger<LotteryController> logger)
        {
            _lotteryService = lotteryService;
            _logger = logger;
        }

        [HttpGet("generate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int[]))]
        public IActionResult GenerateDraw( )
        {
            var numbers = _lotteryService.GenerateDraw();
            return Ok(numbers);
        }

        [HttpPost("set-generation-mode")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult SetGenerationMode([FromBody] bool isServerSide)
        {
            _lotteryService.SetGenerationMode(isServerSide);
            var mode = isServerSide ? "Server-side" : "Client-side";
            return Ok(new { Message = $"{mode} generation mode enabled." });
        }

        [HttpPost("save")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SaveDraw([FromBody] int[] numbers)
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

        [HttpGet("history")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DrawLog>))]
        public async Task<IActionResult> GetDrawHistory( )
        {
            var history = await _lotteryService.GetDrawHistoryAsync();
            return Ok(history);
        }
    }
}
