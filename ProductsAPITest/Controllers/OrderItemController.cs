using Microsoft.AspNetCore.Mvc;
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
    public class OrderItemController : ControllerBase
    {
        private readonly IRepository<OrderItem, Guid> _orderItemRepository;

        public OrderItemController(IRepository<OrderItem, Guid> orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_orderItemRepository.GetAll());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(Guid id)
        {
            var orderItem = _orderItemRepository.GetById(id);
            if (orderItem != null)
            {
                return Ok(_orderItemRepository.GetById(id));
            }
            return NotFound($"OrderItem with ID {id} not found");
        }

        [HttpPost]
        public IActionResult Create(OrderItem orderItem)
        {
            _orderItemRepository.Add(orderItem);
            _orderItemRepository.Save();
            return Created(HttpContext.Request.Scheme + "://"
                + HttpContext.Request.Host + HttpContext.Request.Path + "/"
                + orderItem.id, orderItem);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(Guid id)
        {
            var orderItem = _orderItemRepository.GetById(id);
            if (orderItem != null)
            {
                _orderItemRepository.Remove(orderItem);
                _orderItemRepository.Save();
                return Ok();
            }
            return NotFound($"OrderItem with ID {id} not found");
        }
        [HttpPatch]
        [Route("{id}")]
        public IActionResult Edit(Guid id, OrderItem orderItem)
        {
            var existingOrderItem = _orderItemRepository.GetById(id);
            if (existingOrderItem != null)
            {
                orderItem.id = existingOrderItem.id;
                _orderItemRepository.Update(orderItem);
                return Ok();
            }
            return NotFound($"OrderItem with ID {id} was not found");
        }
    }
}
