using AutoMapper;
using Data.DtoModels;
using Data.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
            var result1 = false;
            try
            {
                var parameters = new SqlParameter("@Username", userName);
                var result = (await _context.Database
                   .ExecuteSqlRawAsync("EXEC CheckUsernameExists @Username",
                        new[] { new SqlParameter("@Username", userName) }));
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return result1;
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
