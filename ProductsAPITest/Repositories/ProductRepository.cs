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
        public Product Add(Product entity)
        {
            entity.Id = Guid.NewGuid();
            context.Products.Add(entity);
            return entity;
        }

        public Product GetById(Guid id)
        {
            return context.Products.Find(id);
        }

        public List<Product> GetAll()
        {
            return context.Products.ToList();
        }

        public void Remove(Product entity)
        {
            context.Products.Remove(entity);
        }

        public Product Update(Product entity)
        {
            context.Products.Update(entity);
            return entity;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
