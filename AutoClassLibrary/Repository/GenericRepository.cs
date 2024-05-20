using AutoClassLibrary.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoClassLibrary.Repository
{
    public class GenericRepository<T> : IDisposable, IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _dbContext;

        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> Add1Async(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task Update1Async(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete1Async(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
        public virtual async Task<T> GetByIdsAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id)
   ;
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            // _logger.LogInformation("ListAllAsync Initiated");
            return await _dbContext.Set<T>().ToListAsync();
        }


        public void Dispose()
        {

        }
    }
}
