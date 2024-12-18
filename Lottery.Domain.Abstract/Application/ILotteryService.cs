using Lottery.Domain.Models.Database.Entities;

namespace Lottery.Domain.Abstract.Application
{
    public interface ILotteryService
    {
        int[] GenerateDraw( );
        Task<IReadOnlyList<DrawLog>> GetDrawHistoryAsync( );
        Task SaveDrawAsync(int[] numbers);
        void SetGenerationMode(bool isServerSide);
    }
}
