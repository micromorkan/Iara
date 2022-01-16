using Api.Context;
using Api.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private ApiContext context = null;
        public Repository(ApiContext _context)
        {
            this.context = _context;
        }

        public async Task<IEnumerable<T>> GetAll(string include = "")
        {
            if (include != string.Empty)
            {
                return await context.Set<T>().Include(include).AsNoTracking().ToListAsync();
            }
            else
            {
                return await context.Set<T>().AsNoTracking().ToListAsync();
            }
        }

        public async Task<T> GetById(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetByFilter(Expression<Func<T, bool>> predicate, string includeTable = "")
        {
            var result = context.Set<T>().Where(predicate);

            if (includeTable != "")
            {
                return await result.Include(includeTable).ToListAsync();
            }
            else
            {
                return await result.ToListAsync();
            }
        }

        public async Task Insert(T obj)
        {
            await context.Set<T>().AddAsync(obj);
            await context.SaveChangesAsync();
        }

        public async Task Update(int id, T obj)
        {
            context.Set<T>().Update(obj);
            await context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}
