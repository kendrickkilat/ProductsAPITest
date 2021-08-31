using Microsoft.AspNetCore.Mvc;
using ProductsAPITest.Models;
using ProductsAPITest.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductsAPITest.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PricingController : ControllerBase
    {
        private readonly IRepository<Pricing, Guid> _pricingRepository;

        public PricingController(IRepository<Pricing, Guid> pricingRepository)
        {
            _pricingRepository = pricingRepository;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_pricingRepository.GetAll());
        }

        // GET api/<ValuesController>/5
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(Guid id)
        {
            var pricing = _pricingRepository.GetById(id);
            if(pricing != null)
            {
                return Ok(pricing);
            }
            return NotFound($"Order with ID {id} was not found");
        }

        // POST api/<ValuesController>
        [HttpPost]
        public IActionResult Create(Pricing pricing)
        {
            _pricingRepository.Add(pricing);
            _pricingRepository.Save();
            return Created(HttpContext.Request.Scheme + "://"
                + HttpContext.Request.Host + HttpContext.Request.Path + "/"
                + pricing.id, pricing);
        }

        // PUT api/<ValuesController>/5
        [HttpPatch]
        [Route("{id}")]
        public IActionResult Edit(Guid id, Pricing pricing)
        {
            var existingPricing = _pricingRepository.GetById(id);
            if(existingPricing != null)
            {
                existingPricing.Price = pricing.Price;
                existingPricing.StartDate = pricing.StartDate;
                existingPricing.EndDate = pricing.EndDate;
                _pricingRepository.Update(existingPricing);
                _pricingRepository.Save();
            }
            return NotFound($"Pricing with ID {id} not found");
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(Guid id)
        {
            var pricing = _pricingRepository.GetById(id);
            if(pricing != null)
            {
                _pricingRepository.Remove(pricing);
                _pricingRepository.Save();
                return Ok();
            }
            return NotFound($"Pricing with ID {id} was not found");
        }
    }
}
