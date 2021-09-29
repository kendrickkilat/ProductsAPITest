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
    //[ApiKey]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IService<ProductDto, Guid> _productService;

        public ProductsController(IService<ProductDto, Guid> productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await _productService.GetById(id);
            if(product == null)
            {
                return NotFound($"Product with ID {id} was not found");
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDto product)
        {
            var link = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}/{product.ProductId}";
            await _productService.Add(product);
            return Created(link, product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _productService.Remove(id);
            if(result != Messages.SUCCESS)
            {
                return NotFound($"Product with ID {id} was not found");
            }

            return Ok("Delete Successful");
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> Edit(Guid id, ProductDto product)
        {
            var result = await _productService.Update(id, product);
            if(result != Messages.SUCCESS)
            {
                return NotFound(result);
            }

            return Ok("Update Successful");
        }
    }
}
