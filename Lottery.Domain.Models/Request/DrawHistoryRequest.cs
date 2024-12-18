using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lottery.Domain.Models.Constants.OrderableProperties;

namespace Lottery.Domain.Models.Request
{
    public class DrawHistoryRequest
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public string OrderBy { get; set; } = DrawLogOrderableProperties.Created;
        public bool Descending { get; set; } = true;
        public int? OrderByNumberIndex { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? MaxNumber {  get; set; }
        public int? MinNumber { get; set; }
    }
}
