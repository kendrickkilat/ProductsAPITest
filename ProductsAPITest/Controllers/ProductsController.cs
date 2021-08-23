﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsAPITest.Models;
using ProductsAPITest.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPITest.Controllers
{
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("api/[controller]")]

        public IActionResult GetProducts()
        {
            return Ok(_productService.GetProducts());
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public IActionResult GetProduct(Guid id)
        {
            var product = _productService.GetProduct(id);
            if(product != null)
            {
                return Ok(_productService.GetProduct(id));
            }
            return NotFound($"Product with ID {id} was not found");
        }

        [HttpPost]
        [Route("api/[controller]")]
        public IActionResult CreateProduct(Product product)
        {
            _productService.AddProduct(product);
            return Created(HttpContext.Request.Scheme + "://" +HttpContext.Request.Host + HttpContext.Request.Path + "/" + product.Id, product);
        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public IActionResult DeleteProduct(Guid id)
        {
            var product = _productService.GetProduct(id);
            if(product != null)
            {
                _productService.DeleteProduct(product);
                return Ok();
            }
            return NotFound($"Product with ID {id} was not found");
        }
        [HttpPatch]
        [Route("api/[controller]/{id}")]
        public IActionResult EditProduct(Guid id, Product product)
        {
            var existingProduct = _productService.GetProduct(id);
            if(existingProduct != null)
            {
                product.Id = existingProduct.Id;
                _productService.EditProduct(product);
                return Ok();
            }
            return NotFound($"Product with ID {id} was not found");
        }
    }
}
