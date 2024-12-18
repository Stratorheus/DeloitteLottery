using Lottery.Domain.Models.Common;
using Lottery.Domain.Models.Database.Entities;
using Lottery.Domain.Models.Dto;
using Lottery.Domain.Models.Request;

namespace Lottery.Domain.Abstract.Application
{
    public interface ILotteryService
    {
        int[] GenerateDraw( );
        Task<PagedResult<DrawLogDto>> GetDrawHistoryAsync(DrawHistoryRequest request);
        bool IsServerSideGeneration( );
        Task SaveDrawAsync(int[] numbers);
        void SetGenerationMode(bool isServerSide);
    }
}
