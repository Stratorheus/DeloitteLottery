using Lottery.Domain.Abstract.Infrastructure.Database;
using Lottery.Domain.Models.Common;
using Lottery.Domain.Models.Database.Entities;
using Lottery.Domain.Specifications.Abstract;

using Microsoft.EntityFrameworkCore;

namespace Lottery.Infrastructure.Database.Repositories
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : DbEntityBase
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        // Using a generic DbContext instead of directly referencing LotteryDbContext.
        // Although this application doesn't currently need it, this approach makes the base repository more flexible.
        // If we wanted to use this base with multiple contexts, we should create derived repository bases for each specific context.
        public RepositoryBase(DbContext dbContext)
        {
            _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _dbSet = _context.Set<T>( );
        }

        public virtual async Task<T?> GetAsync(Guid id)
        {
            //Another option would be _dbSet.FindAsync(id)
            //FindAsync goes directly through the primary key, but returns tracked entity
            //Choosing this approach to keep the idea of returning only untracked entities on read actions
            return await Get().FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<T?> GetAsync(ISpecification<T> spec)
        {
            var query = ApplySpecification(spec);
            return await query.FirstOrDefaultAsync( );
        }

        public virtual async Task<IReadOnlyList<T>> GetAllAsync( )
        {
            return await Get( ).ToListAsync( );
        }

        public virtual async Task<IReadOnlyList<T>> GetAllAsync(ISpecification<T> spec)
        {
            var query = ApplySpecification(spec);
            return await query.ToListAsync( );
        }

        public virtual async Task<PagedResult<T>> GetPagedAsync(ISpecification<T> spec, int pageIndex, int pageSize)
        {
            var query = ApplySpecification(spec);
            var totalCount = await query.CountAsync();

            int skip = pageIndex * pageSize;
            var items = await query
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<T>(items, totalCount, pageIndex, pageSize);
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            _dbSet.Add(entity);
            await Save( );
            return entity;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await Save( );
            return entity;
        }

        public virtual async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await Save( );
        }

        protected async Task Save( )
        {
            await _context.SaveChangesAsync( );
        }

        protected IQueryable<T> Get( )
        {
            return _dbSet.AsNoTracking( );
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            IQueryable<T> query = Get( );

            if ( spec.Criteria != null )
            {
                query = query.Where(spec.Criteria);
            }

            if ( spec.OrderBy != null )
            {
                query = query.OrderBy(spec.OrderBy);
            }
            else if ( spec.OrderByDescending != null )
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }

            foreach ( var include in spec.Includes )
            {
                query = query.Include(include);
            }

            return query;
        }
    }
}
