using EV_Station.Application.Common.Abstractions.IRepositories.IBaseRepositories;
using EV_Station.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

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
        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
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
