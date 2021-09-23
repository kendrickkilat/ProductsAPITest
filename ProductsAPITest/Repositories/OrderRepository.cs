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
        }
        //public async Task<Order> GetOrderById(Guid id)
        //{
        //    //var order = await context.Orders
        //    //                            .Include(o => o.orderItem)
        //    //                                .ThenInclude(prod => prod.products)
        //    //                            .Where(ord => ord.id == id)
        //    //                            .FirstOrDefault();
        //    //return order;
        //}
    }
}
