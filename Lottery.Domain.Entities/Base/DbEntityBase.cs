using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Domain.Entities.Base
{
    public abstract class DbEntityBase
    {
        public Guid Id { get; set; }
    }
}
