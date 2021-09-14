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
    //[ApiKey]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IService<Product, Guid> _productService;

        public ProductsController(IService<Product, Guid> productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productService.GetAll());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await _productService.GetById(id);
            if(product != null)
            {
                return Ok(product);
            }
            return NotFound($"Product with ID {id} was not found");
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            var link = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}/{product.Id}";
            await _productService.Add(product);
            return Created(link, product);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _productService.Remove(id);
            if(result == "Success")
            {
                return Ok("Delete Successful");
            }
            return NotFound($"Product with ID {id} was not found");
        }
        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> Edit(Guid id, Product product)
        {
            var result = await _productService.Update(id, product);
            if(result == "Success")
            {
                return Ok("Update Successful");
            }
            return NotFound(result);
        }
    }
}
