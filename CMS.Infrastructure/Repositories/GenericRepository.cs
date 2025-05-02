using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Domain.Interfaces;
using CMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CMS.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
            => await dbContext.Set<T>().ToListAsync();

        public async Task<T> GetByIdAsync(object id)
            => await dbContext.Set<T>().FindAsync(id);

        public async Task AddAsync(T entity)
         => await dbContext.Set<T>().AddAsync(entity);
        public void Delete(T entity)
            => dbContext.Set<T>().Remove(entity);
        public void Update(T entity)
            => dbContext.Set<T>().Update(entity);

        public async Task<int> SaveChangesAsync()
            => await dbContext.SaveChangesAsync();
    }
}
