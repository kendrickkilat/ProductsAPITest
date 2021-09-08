using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsAPITest.Attributes;
using ProductsAPITest.Models;
using ProductsAPITest.Repositories;
using ProductsAPITest.Services;
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
        private readonly IService<Order, Guid> _orderService;

        public OrderController(IService<Order, Guid> orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public IActionResult Create(Order order)
        {
            var link = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}/{order.id}";
            _orderService.Add(order);
            return Created(link, order);

        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(Guid id)
        {
            var result = _orderService.Remove(id);
            if(result == "Success")
            {
                return Ok("Delete Successful");
            }
            return NotFound($"Order with ID {id} was not found");
        }
        [HttpPatch]
        [Route("{id}")]
        public IActionResult Edit(Guid id, Order order)
        {
            var result = _orderService.Update(id, order);
            if (result == "Success")
            {
               return Ok("Update Successful");
            }
            return NotFound(result);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(Guid id)
        {
            var order = _orderService.GetById(id);
            if(order != null)
            {
                return Ok(order);
            }
            return NotFound($"Order with ID {id} was not found");
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_orderService.GetAll());
        }
    }
}
