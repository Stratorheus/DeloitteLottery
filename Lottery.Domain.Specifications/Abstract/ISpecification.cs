﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using Lottery.Domain.Entities.Base;

namespace Lottery.Domain.Specifications.Abstract
{
    internal interface ISpecification<T> where T : DbEntityBase
    {
        Expression<Func<T, bool>>? Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        Expression<Func<T, object>>? OrderBy { get; }
        Expression<Func<T, object>>? OrderByDescending { get; }
        int? Take { get; }
        int? Skip { get; }
        bool PagingEnabled { get; }
    }
}