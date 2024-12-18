using System.Linq.Expressions;
using Lottery.Domain.Models.Database.Entities;

namespace Lottery.Domain.Specifications.Abstract
{
    public abstract class CriteriaSpecificationBase<T> : SpecificationBase<T> where T : DbEntityBase
    {
        protected CriteriaSpecificationBase(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria ?? throw new ArgumentNullException(nameof(criteria));
        }
    }
}
