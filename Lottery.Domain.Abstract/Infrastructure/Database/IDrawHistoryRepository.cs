using Lottery.Domain.Models.Database.Entities;

namespace Lottery.Domain.Abstract.Infrastructure.Database
{
    public interface IDrawHistoryRepository : IRepository<DrawLog>
    {
    }
}
