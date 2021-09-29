using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsAPITest.Attributes;
using ProductsAPITest.Constants;
using ProductsAPITest.Dtos;
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
        private readonly IService<OrderDto, Guid> _orderService;

        public OrderController(IService<OrderDto, Guid> orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderDto order)
        {
            var link = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}/{order.OrderId}";
            return Created(link, await _orderService.Add(order));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _orderService.Remove(id);
            if(result != Messages.SUCCESS)
            {
                return NotFound($"Order with ID {id} was not found");
            }

            return Ok("Delete Successful");
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> Edit(Guid id, OrderDto order)
        {
            var result = await _orderService.Update(id, order);
            if (result != Messages.SUCCESS)
            {
               return NotFound(result);
            }

            return Ok("Update Successful");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var order = await _orderService.GetById(id);
            if(order == null)
            {
                return NotFound($"Order with ID {id} was not found");
            }

            return Ok(order);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _orderService.GetAll());
        }
    }
}
