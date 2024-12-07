using AutoMapper;
using Data.DtoModels;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IMapper _mapper;
        private readonly BarbershopDbContext _context;
        private readonly ConcurrentDictionary<Type, object> _repositories = new();

        public UnitOfWork(BarbershopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            return (IRepository<TEntity>)_repositories.GetOrAdd(typeof(TEntity), _ => new Repository<TEntity>(_context));
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<bool> CheckUsernameExistsAsync(string userName)
        {
            var result = false;
            try
            {
                result = await _context.Users.AnyAsync(u => u.Username == userName);

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return result;

        }

        public async Task<List<QueueEntry>> GetAllQueueEntriesAsync(string? customerName, DateTime? requestedTime)
        {
            try
            {
                //I Create this PROCEDURE In The DB and I used the abilities of Entity Framework the execute the PROCEDURE for every filter request or defult list

                //CREATE PROCEDURE GetBarbershopQueue
                //    @RequestedTime DATETIME = NULL,
                //    @CustomerName NVARCHAR(100) = NULL
                //AS
                //BEGIN
                //    SELECT
                //        Id,
                //        CustomerId,  --Ensure this column is included
                //        CustomerName,
                //        RequestedTime,
                //        CreatedAt
                //    FROM
                //        QueueEntries
                //    WHERE
                //        (@RequestedTime IS NULL OR CONVERT(DATE, RequestedTime) = CONVERT(DATE, @RequestedTime))
                //        AND(@CustomerName IS NULL OR CustomerName LIKE '%' + @CustomerName + '%')
                //    ORDER BY
                //        RequestedTime ASC;
                //                END;

                var result = await _context.QueueEntries
                    .FromSqlInterpolated($"EXEC GetBarbershopQueue @RequestedTime={requestedTime}, @CustomerName={customerName}")
                    .ToListAsync();

                return result;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public TDestination Mapper<TSource, TDestination>(TSource model)
        {
            return _mapper.Map<TDestination>(model);
        }

        public async Task<TEntity> FirstOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

    }
}
