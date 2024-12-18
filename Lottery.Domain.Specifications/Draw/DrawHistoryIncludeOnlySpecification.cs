using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lottery.Domain.Models.Database.Entities;
using Lottery.Domain.Specifications.Abstract;

namespace Lottery.Domain.Specifications.Draw
{
    public class DrawHistoryIncludeOnlySpecification : SpecificationBase<DrawLog>
    {
        public DrawHistoryIncludeOnlySpecification()
        {
            AddInclude(x => x.DrawNumbers);
        }
    }
}
