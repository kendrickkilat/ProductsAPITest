using ProductsAPITest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPITest.Repositories
{
    public class OrderItemRepository : IRepository<OrderItem, Guid>
    {
        private readonly ProductContext context;

        public OrderItemRepository(ProductContext context)
        {
            this.context = context;
        }

        public OrderItem Add(OrderItem entity)
        {
            entity.id = Guid.NewGuid();
            context.OrderItems.Add(entity);
            return entity;
        }

        public List<OrderItem> GetAll()
        {
            return context.OrderItems.ToList();
        }

        public OrderItem GetById(Guid id)
        {
            return context.OrderItems.Find(id);
        }

        public void Remove(OrderItem entity)
        {
            var existingProduct = context.OrderItems.Find(entity.id);
            if (existingProduct != null)
            {
                context.OrderItems.Remove(entity);
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public OrderItem Update(OrderItem entity)
        {
            var existingProduct = context.Products.Find(entity.id);
            if (existingProduct != null)
            {
                context.OrderItems.Update(entity);
            }
            return entity;
        }
    }
}
