using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Domain.Models.Database.Entities
{
    public abstract class DbEntityBase
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        public Guid Id { get; set; }
    }
}
