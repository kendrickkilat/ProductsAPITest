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
        public string Add(Order entity)
        {
            _orderRepository.Add(entity);
            _orderRepository.Save();
            return "Success";
        }

        public List<Order> GetAll()
        {
            return _orderRepository.GetAll();
        }

        public Order GetById(Guid id)
        {
            var order = _orderRepository.GetById(id);
            return order;
        }

        public string Remove(Guid id)
        {
            var item = _orderRepository.GetById(id);
            if (item != null)
            {
                _orderRepository.Remove(item);
                _orderRepository.Save();
                return "Success";
            }
            return "Error";
        }


        public string Update(Guid id, Order entity)
        {
            var exist = _orderRepository.GetById(id);
            if (exist != null)
            {
                exist.DateOrdered = entity.DateOrdered;
                exist.OrderAddress = entity.OrderAddress;
                exist.Status = entity.Status;

                _orderRepository.Update(exist);
                _orderRepository.Save();
                return "Success";
            }
            return "Error";
        }
    }
}
