using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lottery.Domain.Abstract.Infrastructure.Database;
using Lottery.Domain.Models.Database.Entities;
using Lottery.Infrastructure.Database.Ef.Context;

namespace Lottery.Infrastructure.Database.Repositories
{
    public sealed class DrawHistoryRepository : RepositoryBase<DrawLog>, IDrawHistoryRepository
    {
        public DrawHistoryRepository(LotteryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
