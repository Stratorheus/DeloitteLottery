using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lottery.Domain.Models.Common;
using Lottery.Domain.Models.Database.Entities;
using Lottery.Domain.Specifications.Abstract;

namespace Lottery.Domain.Abstract.Infrastructure.Database
{
    public interface IRepository<T> where T : DbEntityBase
    {
        /// <summary>
        /// Get entity by its primary key (Guid).
        /// </summary>
        /// <param name="id">Unique identifier of the entity.</param>
        /// <returns>Entity or null if not found.</returns>
        Task<T?> GetAsync(Guid id);

        /// <summary>
        /// Get single entity by specification. Expects 0 or 1 result.
        /// If there can be multiple matches, consider using a List or Paged methods.
        /// </summary>
        /// <param name="spec">Specification defining criteria.</param>
        /// <returns>First entity matching the criteria or null if none match.</returns>
        Task<T?> GetAsync(ISpecification<T> spec);

        /// <summary>
        /// Get all entities (no criteria).
        /// </summary>
        /// <returns>List of all entities.</returns>
        Task<IReadOnlyList<T>> GetAllAsync( );

        /// <summary>
        /// Get all entities matching the given specification.
        /// </summary>
        /// <param name="spec">Specification defining filter, includes, and sorting.</param>
        /// <returns>List of matched entities.</returns>
        Task<IReadOnlyList<T>> GetAllAsync(ISpecification<T> spec);

        /// <summary>
        /// Returns a paginated list of entities matching the given specification.
        /// </summary>
        /// <param name="spec">Specification for filtering, includes and sorting.</param>
        /// <param name="pageIndex">Zero-based page index.</param>
        /// <param name="pageSize">Number of items per page.</param>
        /// <returns>Paged result containing items and total count.</returns>
        Task<PagedResult<T>> GetPagedAsync(ISpecification<T> spec, int pageIndex, int pageSize);

        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
