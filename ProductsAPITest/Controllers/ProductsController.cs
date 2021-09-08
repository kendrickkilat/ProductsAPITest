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
        public IActionResult GetAll()
        {
            return Ok(_productService.GetAll());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(Guid id)
        {
            var product = _productService.GetById(id);
            if(product != null)
            {
                return Ok(product);
            }
            return NotFound($"Product with ID {id} was not found");
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            var link = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}/{product.Id}";
            _productService.Add(product);
            return Created(link, product);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(Guid id)
        {
            var product = _productService.Remove(id);
            if(product == "Success")
            {
                return Ok("Delete Successful");
            }
            return NotFound($"Product with ID {id} was not found");
        }
        [HttpPatch]
        [Route("{id}")]
        public IActionResult Edit(Guid id, Product product)
        {
            var result = _productService.Update(id, product);
            if(result == "Success")
            {
                return Ok("Update Successful");
            }
            return NotFound(result);
        }
    }
}
