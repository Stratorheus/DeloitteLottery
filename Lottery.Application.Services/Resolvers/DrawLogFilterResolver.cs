﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using LinqKit;

using Lottery.Domain.Models.Database.Entities;
using Lottery.Domain.Models.Request;

namespace Lottery.Application.Services.Resolvers
{
    public static class DrawLogFilterResolver
    {
        public static Expression<Func<DrawLog, bool>> CreateCriteria(DrawHistoryRequest filter)
        {
            var criteria = PredicateBuilder.New<DrawLog>(true);
            if ( filter.ToDate.HasValue ) criteria.And(x => x.Created <= filter.ToDate);
            if ( filter.FromDate.HasValue ) criteria.And(x => x.Created >= filter.FromDate);
            if ( filter.MaxNumber.HasValue ) criteria.And(x => x.DrawNumbers.All(y => y.Number <= filter.MaxNumber));
            if ( filter.MinNumber.HasValue ) criteria.And(x => x.DrawNumbers.All(y => y.Number >= filter.MinNumber));
            return criteria;
        }
    }
}