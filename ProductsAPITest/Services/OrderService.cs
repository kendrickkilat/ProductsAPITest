using AutoMapper;
using ProductsAPITest.Dtos;
using ProductsAPITest.Models;
using ProductsAPITest.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProductsAPITest.Services
{
    public class OrderService : IService<OrderDto, Guid>
    {
        private readonly IRepository<Order, Guid> _orderRepository;
        private readonly IMapper mapper;

        public OrderService(IRepository<Order, Guid> orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            this.mapper = mapper;
        }
        public async Task<string> Add(OrderDto entity)
        {
            entity.id = Guid.NewGuid();
            var order = mapper.Map<Order>(entity);
            await _orderRepository.Add(order);
            await _orderRepository.Save();
            return "Success";
        }

        public async Task<List<OrderDto>> GetAll()
        {
            var orders = await _orderRepository.GetAll();
            var ordersDto = mapper.Map<List<OrderDto>>(orders);
            return ordersDto;
        }

        public async Task<OrderDto> GetById(Guid id)
        {
            var order = await _orderRepository.GetById(id);
            var orderDto = mapper.Map<OrderDto>(order);
            return orderDto;
        }

        public async Task<string> Remove(Guid id)
        {
            var itemDto =  await this.GetById(id);
            if (itemDto != null)
            {
                var item = mapper.Map<Order>(itemDto);
                await _orderRepository.Remove(item);
                await _orderRepository.Save();
                return "Success";
            }
            return "Error";
        }


        public async Task<string> Update(Guid id, OrderDto entityDto)
        {
            try
            {
                var order = await _orderRepository.GetById(id);
                if (order != null)
                {
                    var entity = mapper.Map<Order>(entityDto);
                    //var exist = mapper.Map<Order>(existDto);
                    //entity.id = order.id;
                    //order.DateOrdered = entity.DateOrdered;
                    //order.OrderAddress = entity.OrderAddress;
                    //order.Status = entity.Status;
                    await _orderRepository.Update(entity);
                    await _orderRepository.Save();
                    return "Success";
                }
                return "Error";
            }
            catch(Exception e)
            {
                return e.Message;
            }
           
        }
    }
}
