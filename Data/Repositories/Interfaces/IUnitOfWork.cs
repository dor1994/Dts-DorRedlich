using Data.DtoModels;
using System.Linq.Expressions;

namespace Data.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity : class;
        Task<int> SaveChangesAsync();

        Task<bool> CheckUsernameExistsAsync(string userName);

        TDestination Mapper<TSource, TDestination>(TSource model);
        Task<TEntity> FirstOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;

        public Task<List<QueueEntry>> GetAllQueueEntriesAsync(string? customerName, DateTime? requestedTime);

    }
}
