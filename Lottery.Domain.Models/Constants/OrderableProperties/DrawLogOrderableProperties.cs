using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Domain.Models.Constants.OrderableProperties
{
    public static class DrawLogOrderableProperties
    {
        public const string Created = "Created";
        public const string Id = "Id";
        public const string NumberIndex = "NumberIndex";

        public static IEnumerable<string> GetAll() => new List<string>( ) { Created, Id, NumberIndex };
    }
}
