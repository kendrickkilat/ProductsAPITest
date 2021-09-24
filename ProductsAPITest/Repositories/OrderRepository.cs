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
                                    .Include(p => p.OrderItem)
                                    .ToListAsync();
            return orders;
        }
    }
}
