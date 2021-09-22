using Microsoft.EntityFrameworkCore;
using ProductsAPITest.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductsAPITest.Repositories
{
    public class BaseRepository<T1, T2> : IRepository<T1, T2> where T1:class
    {
        private readonly ProductContext context;

        public BaseRepository(ProductContext context)
        {
            this.context = context;
        }

        public async Task<T1> Add(T1 entity)
        {
            await context.Set<T1>().AddAsync(entity);
            return entity;
        }

        public DbSet<T1> Entity()
        {
            return context.Set<T1>();
        }

        public async Task<List<T1>> GetAll()
        {
            return await context.Set<T1>().ToListAsync();
        }

        public async Task<T1> GetById(T2 id)
        {
            return await context.Set<T1>().FindAsync(id);
        }

        public async Task<bool> Remove(T1 entity)
        {
            context.Set<T1>().Remove(entity);
            return await context.Set<T1>().AnyAsync();
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public async Task<bool> Update(T1 entity)
        {
            //context.Set<T1>().Update(entity);
            context.Entry(entity).CurrentValues.SetValues(entity);
            await context.SaveChangesAsync();
            return await context.Set<T1>().AnyAsync();
        }
    }
}
