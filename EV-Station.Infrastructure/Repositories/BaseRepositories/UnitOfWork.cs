using EV_Station.Application.Common.Abstractions.IRepositories;
using EV_Station.Application.Common.Abstractions.IRepositories.IBaseRepositories;
using EV_Station.Infrastructure.Persistence.SqlServer.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace EV_Station.Infrastructure.Repositories.BaseRepositories
{
    public class UnitOfWork(EVStationDbContext context) : IUnitOfWork
    {
        private readonly EVStationDbContext _context = context;
        private readonly Dictionary<Type, object> _repositories = new();
        private IDbContextTransaction? _transaction;

        private IUserRepository? _userRepository;
        private IRoleRepository? _roleRepository;
        private IProviderRepository? _providerRepository;

        public IUserRepository Users
        {
            get
            {
                return _userRepository ??= new UserRepository(_context);
            }
        }

        public IRoleRepository Roles
        {
            get
            {
                return _roleRepository ??= new RoleRepository(_context);
            }
        }

        public IProviderRepository Providers
        {
            get
            {
                return _providerRepository ??= new ProviderRepository(_context);
            }
        }

        public IGenericRepository<T> Repository<T>() where T : class
        {
            if (_repositories.ContainsKey(typeof(T)))
                return (IGenericRepository<T>)_repositories[typeof(T)];

            var repo = new GenericRepository<T>(_context);
            _repositories[typeof(T)] = repo;
            return repo;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => await _context.SaveChangesAsync(cancellationToken);

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context?.Dispose();
        }
    }
}
