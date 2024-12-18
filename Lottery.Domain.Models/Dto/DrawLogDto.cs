using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lottery.Domain.Models.Database.Entities;

namespace Lottery.Domain.Models.Dto
{
    public class DrawLogDto
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public IEnumerable<DrawNumberDto> Numbers { get; set; }
    }
}
