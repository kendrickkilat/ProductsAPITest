using ProductsAPITest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPITest.Repositories
{
    public class OrderRepository : IRepository<Order, Guid>
    {
        private readonly ProductContext context;
        public OrderRepository(ProductContext context)
        {
            this.context = context;
        }
        public Order Add(Order entity)
        {
            entity.id = Guid.NewGuid();
            context.Orders.Add(entity);
            return entity;
        }

        public List<Order> GetAll()
        {
            return context.Orders.ToList();
        }

        public Order GetById(Guid id)
        {
            return context.Orders.Find(id);
        }

        public void Remove(Order entity)
        {
            var existingOrder = context.Orders.Find(entity.id);
            if(existingOrder != null)
            {
                context.Orders.Remove(entity);
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public Order Update(Order entity)
        {
            var existingOrder = context.Orders.Find(entity.id);
            if (existingOrder != null)
            {
                context.Orders.Update(entity);
            }
            return entity;
        }
    }
}
