namespace EV_Station.Application.Common.Abstractions.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> Repository<T>() where T : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
