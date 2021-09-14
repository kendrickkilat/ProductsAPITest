using Microsoft.EntityFrameworkCore;
using ProductsAPITest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPITest.Repositories
{
    public class ProductRepository : IRepository<Product, Guid>
    {
        private readonly ProductContext context;
        public ProductRepository(ProductContext context)
        {
            this.context = context;
        }
        public async Task<Product> Add(Product entity)
        {
            entity.Id = Guid.NewGuid();
            await context.Products.AddAsync(entity);
            return entity;
        }

        public async Task<Product> GetById(Guid id)
        {
            return await context.Products.FindAsync(id);
        }

        public async Task<List<Product>> GetAll()
        {
            return  await context.Products.ToListAsync();
        }

        public void Remove(Product entity)
        {
            context.Products.Remove(entity);
        }

        public void Update(Product entity)
        {
            context.Products.Update(entity);
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}
