using ProductsAPITest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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
        public async Task<Order> Add(Order entity)
        {
            entity.id = Guid.NewGuid();
            await context.Orders.AddAsync(entity);
            return entity;
        }

        public async Task<List<Order>> GetAll()
        {
            return await context.Orders.ToListAsync();
        }

        public async Task<Order> GetById(Guid id)
        {
            return  await context.Orders.FindAsync(id);
        }

        public void Remove(Order entity)
        {
           context.Orders.Remove(entity);
        }

        public async Task Save()
        {
           await context.SaveChangesAsync();
        }

        public void Update(Order entity)
        {
           context.Orders.Update(entity);
        }
    }
}
