using ProductsAPITest.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPITest.Models
{
    public interface IOrderRepository : IRepository<Order, Guid>
    {
        Task<List<Order>> GetAllWithProducts();
        Task<Order> GetByIdWithProducts(Guid id);
    }
}
