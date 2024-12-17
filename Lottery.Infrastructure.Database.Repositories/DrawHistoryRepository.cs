using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lottery.Domain.Models.Database.Entities;
using Lottery.Infrastructure.Database.Ef.Context;

namespace Lottery.Infrastructure.Database.Repositories
{
    public sealed class DrawHistoryRepository : RepositoryBase<DrawLog>
    {
        public DrawHistoryRepository(LotteryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
