using EV_Station.Application.Common.Abstractions.IRepositories.IBaseRepositories;
using EV_Station.Infrastructure.Persistence.SqlServer.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EV_Station.Infrastructure.Repositories.BaseRepositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly EVStationDbContext _context;
        protected readonly DbSet<T> _dbSet;
        public GenericRepository(EVStationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task<T?> GetByIdAsync(Guid id,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);
        }

        public async Task<IEnumerable<T>> GetAllAsync(
             params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
