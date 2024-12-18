using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using Lottery.Domain.Models.Constants.OrderableProperties;
using Lottery.Domain.Models.Database.Entities;
using Lottery.Domain.Models.Request;
using Lottery.Domain.Specifications.Abstract;

namespace Lottery.Domain.Specifications.Draw
{
    public class DrawHistoryByFilterCriteriaSpecification : CriteriaSpecificationBase<DrawLog>
    {
        public DrawHistoryByFilterCriteriaSpecification(Expression<Func<DrawLog, bool>> criteria, DrawHistoryRequest request) : base(criteria)
        {
            AddInclude(x => x.DrawNumbers);

            var orderByExpression = ResolveOrderBy(request);
            if ( request.Descending )
                ApplyOrderByDescending(orderByExpression);
            else
                ApplyOrderBy(orderByExpression);
        }

        private static Expression<Func<DrawLog, object>> ResolveOrderBy(DrawHistoryRequest request)
        {
            return request.OrderBy switch
            {
                DrawLogOrderableProperties.Created => log => log.Created,
                DrawLogOrderableProperties.Id => log => log.Id,
                DrawLogOrderableProperties.NumberIndex when request.OrderByNumberIndex.HasValue =>
                    log => log.DrawNumbers
                        .Where(n => n.Index == request.OrderByNumberIndex.Value)
                        .Select(n => (int?)n.Number)
                        .FirstOrDefault( ) ?? (request.Descending ? int.MinValue : int.MaxValue), //Use sentinel value to put this record to the end
                _ => log => log.Created
            };
        }
    }
}
