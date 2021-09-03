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
    //[ApiKey]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IRepository<Product, Guid> _productRepository;

        public ProductsController(IRepository<Product, Guid> productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_productRepository.GetAll());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(Guid id)
        {
            var product = _productRepository.GetById(id);
            if(product != null)
            {
                return Ok(_productRepository.GetById(id));
            }
            return NotFound($"Product with ID {id} was not found");
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            _productRepository.Add(product);
            _productRepository.Save();
            return Created(HttpContext.Request.Scheme + "://" 
                + HttpContext.Request.Host + HttpContext.Request.Path + "/" 
                + product.Id, product);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(Guid id)
        {
            var product = _productRepository.GetById(id);
            if(product != null)
            {
                _productRepository.Remove(product);
                _productRepository.Save();
                return Ok();
            }
            return NotFound($"Product with ID {id} was not found");
        }
        [HttpPatch]
        [Route("{id}")]
        public IActionResult Edit(Guid id, Product product)
        {
            var existingProduct = _productRepository.GetById(id);
            if(existingProduct != null)
            {
                product.Id = existingProduct.Id;
                _productRepository.Update(product);
                return Ok();
            }
            return NotFound($"Product with ID {id} was not found");
        }
    }
}
