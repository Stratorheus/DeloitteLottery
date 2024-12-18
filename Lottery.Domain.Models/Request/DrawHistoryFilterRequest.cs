using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Domain.Models.Request
{
    public class DrawHistoryFilterRequest
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; } = "Created";
        public bool SortDescending { get; set; } = true;
        public DateTime? FilterFromDate { get; set; }
        public DateTime? FilterToDate { get; set; }
    }
}
