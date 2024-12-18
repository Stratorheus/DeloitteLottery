using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Domain.Models.Database.Entities
{
    public sealed class DrawNumber : DbEntityBase
    {
        //In any other case, there should be N:M relation
        //Choosing 1:N relation, because althought there could be data redundancy, due to app specs this approach 
        //should still be faster, or at least easier to manage in code
        public int DrawLogId { get; set; }
        public DrawLog DrawLog { get; set; }
        public int Number { get; set; }
        public int Index { get; set; }
    }
}
