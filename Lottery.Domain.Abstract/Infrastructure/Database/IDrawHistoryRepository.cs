using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lottery.Domain.Models.Database.Entities;

namespace Lottery.Domain.Abstract.Infrastructure.Database
{
    public interface IDrawHistoryRepository : IRepository<DrawLog>
    {
    }
}
