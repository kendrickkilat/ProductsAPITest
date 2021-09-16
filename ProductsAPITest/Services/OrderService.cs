using ProductsAPITest.Models;
using ProductsAPITest.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPITest.Services
{
    public class OrderService : IService<Order, Guid>
    {
        private readonly IRepository<Order, Guid> _orderRepository;

        public OrderService(IRepository<Order, Guid> orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<string> Add(Order entity)
        {
            entity.id = Guid.NewGuid();
            await _orderRepository.Add(entity);
            await _orderRepository.Save();
            return "Success";
        }

        public async Task<List<Order>> GetAll()
        {
            return await _orderRepository.GetAll();
        }

        public async Task<Order> GetById(Guid id)
        {
            var order = await _orderRepository.GetById(id);
            return order;
        }

        public async Task<string> Remove(Guid id)
        {
            var item =  await _orderRepository.GetById(id);
            if (item != null)
            {
                var res = await _orderRepository.Remove(item);
                await _orderRepository.Save();
                return "Success";
            }
            return "Error";
        }


        public async Task<string> Update(Guid id, Order entity)
        {
            var exist = await _orderRepository.GetById(id);
            if (exist != null)
            {
                exist.DateOrdered = entity.DateOrdered;
                exist.OrderAddress = entity.OrderAddress;
                exist.Status = entity.Status;

               var res = await _orderRepository.Update(exist);
               await _orderRepository.Save();
                return "Success";
            }
            return "Error";
        }
    }
}
