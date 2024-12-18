using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Domain.Models.Database.Entities
{
    public sealed class DrawNumber : DbEntityBase
    {
        public int DrawLogId { get; set; }
        public DrawLog DrawLog { get; set; }
        public int Number { get; set; }
        public int Index { get; set; }
    }
}
