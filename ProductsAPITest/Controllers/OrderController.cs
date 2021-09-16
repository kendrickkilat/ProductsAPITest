using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsAPITest.Attributes;
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
        private readonly IService<Order, Guid> _orderService;
        private readonly IMapper mapper;

        public OrderController(IService<Order, Guid> orderService, IMapper mapper)
        {
            _orderService = orderService;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderDto orderDto)
        {
            var link = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}/{orderDto.id}";
            var order = mapper.Map<Order>(orderDto);
            await _orderService.Add(order);
            return Created(link, order);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _orderService.Remove(id);
            if(result == "Success")
            {
                return Ok("Delete Successful");
            }
            return NotFound($"Order with ID {id} was not found");
        }
        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> Edit(Guid id, Order order)
        {
            var result = await _orderService.Update(id, order);
            if (result == "Success")
            {
               return Ok("Update Successful");
            }
            return NotFound(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var order = await _orderService.GetById(id);
            if(order != null)
            {
                return Ok(order);
            }
            return NotFound($"Order with ID {id} was not found");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderService.GetAll();
            var ordersDto = mapper.Map<List<OrderDto>>(orders);
            return Ok(ordersDto);
        }
    }
}
