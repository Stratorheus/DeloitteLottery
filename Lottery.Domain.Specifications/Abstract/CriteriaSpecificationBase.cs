using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Lottery.Domain.Models.Database.Entities;

namespace Lottery.Domain.Specifications.Abstract
{
    internal abstract class CriteriaSpecificationBase<T> : SpecificationBase<T> where T : DbEntityBase
    {
        protected CriteriaSpecificationBase(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria ?? throw new ArgumentNullException(nameof(criteria));
        }
    }
}
