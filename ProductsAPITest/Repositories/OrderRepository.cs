using ProductsAPITest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ProductsAPITest.Repositories
{
    public class OrderRepository : BaseRepository<Order, Guid>, IOrderRepository
    {
        private readonly ProductContext context;
        public OrderRepository(ProductContext context):base(context)
        {
            this.context = context;
        }
        public async Task<List<Order>> GetOrders()
        {
            var orders = await context.Set<Order>()
                                    .Include(o => o.OrderItem)
                                        .ThenInclude(p => p.Products)
                                    .ToListAsync();
            return orders;
        }

        public async Task<Order> GetOrder(Guid id)
        {
            var order = await context.Set<Order>()
                                    .Include(o => o.OrderItem)
                                        .ThenInclude(p => p.Products)
                                    .Where(o => o.OrderId == id)
                                    .FirstOrDefaultAsync();
            return order;
        }
    }
}
