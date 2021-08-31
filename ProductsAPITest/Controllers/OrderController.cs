using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsAPITest.Attributes;
using ProductsAPITest.Models;
using ProductsAPITest.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPITest.Controllers
{
   
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IRepository<Order, Guid> _orderRepository;

        public OrderController(IRepository<Order, Guid> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpPost]
        public IActionResult Create(Order order)
        {
            _orderRepository.Add(order);
            _orderRepository.Save();
            return Created(HttpContext.Request.Scheme + "://"
                + HttpContext.Request.Host + HttpContext.Request.Path + "/"
                + order.id, order);

        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(Guid id)
        {
            var order = _orderRepository.GetById(id);
            if(order != null)
            {
                _orderRepository.Remove(order);
                _orderRepository.Save();
                return Ok();
            }
            return NotFound($"Order with ID {order.id} was not found");
        }
        [HttpPatch]
        [Route("{id}")]
        public IActionResult Edit(Guid id, Order order)
        {
            var existingOrder = _orderRepository.GetById(order.id);
            if (existingOrder != null)
            {
                existingOrder.OrderAddress = order.OrderAddress;
                existingOrder.Status = order.Status;
                _orderRepository.Update(existingOrder);
                _orderRepository.Save();
            }
            return NotFound($"Order with ID {id} was not found");
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(Guid id)
        {
            var order = _orderRepository.GetById(id);
            if(order != null)
            {
                return Ok(order);
            }
            return NotFound($"Order with ID {id} was not found");
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_orderRepository.GetAll());
        }
    }
}
