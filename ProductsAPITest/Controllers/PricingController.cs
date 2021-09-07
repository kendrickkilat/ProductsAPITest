using Microsoft.AspNetCore.Mvc;
using ProductsAPITest.Models;
using ProductsAPITest.Repositories;
using System;

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
            if (pricing != null)
            {
                return Ok(pricing);
            }
            return NotFound($"Order with ID {id} was not found");
        }
        // Checks if price start date and end date is valid
        [HttpPost]
        public IActionResult Create(Pricing pricing)
        {
            var isValid = true;
            var pricings = _pricingRepository.GetAll();
            //Pricing foundPricing = pricings.Find(item => pricing.StartDate.Ticks > item.StartDate.Ticks && pricing.StartDate.Ticks < item.EndDate.Ticks);
            
            foreach (var item in pricings)
            {
                //var cond = pricing.StartDate.Ticks <= item.EndDate.Ticks && item.StartDate.Ticks <= pricing.EndDate.Ticks;
                
                if (pricing.StartDate.Ticks <= item.EndDate.Ticks && item.StartDate.Ticks <= pricing.EndDate.Ticks)
                {
                    isValid = false;
                }
            }
            if (isValid)
            {
                _pricingRepository.Add(pricing);
                _pricingRepository.Save();
                return Created(HttpContext.Request.Scheme + "://"
                    + HttpContext.Request.Host + HttpContext.Request.Path + "/"
                    + pricing.id, pricing);
            }
            return NotFound($"Invalid StartDate or EndDate");
        }

        // PUT api/<ValuesController>/5
        [HttpPatch]
        [Route("{id}")]
        public IActionResult Edit(Guid id, Pricing pricing)
        {
            var existingPricing = _pricingRepository.GetById(id);
            if (existingPricing != null)
            {
                pricing.id = existingPricing.id;
                _pricingRepository.Update(pricing);
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
            if (pricing != null)
            {
                _pricingRepository.Remove(pricing);
                _pricingRepository.Save();
                return Ok();
            }
            return NotFound($"Pricing with ID {id} was not found");
        }
    }
}
