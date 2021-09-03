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
            /**
             * Goal: No conflicts on price validity across all products
             * - Get Start Date and End Date of currently created Pricing
             * - Get a list of all existing pricings
             * - Loop through it all and compare if the created start date is after
             * the compared start date and the created end date is less than the compared end date
             */
            if (pricing.EndDate.Ticks - pricing.StartDate.Ticks > 0)
            {
                _pricingRepository.Add(pricing);
                _pricingRepository.Save();
                return Created(HttpContext.Request.Scheme + "://"
                    + HttpContext.Request.Host + HttpContext.Request.Path + "/"
                    + pricing.id, pricing);
            }
            return NotFound($"Invalid StartDate or EndDate");
        }

        public bool CheckDateValidity(Pricing pricing) 
        {
            var pricings = _pricingRepository.GetAll();
            pricings.ForEach((item) =>
            {
                var psdt = pricing.StartDate.Ticks;
                var pedt = pricing.EndDate.Ticks;
                var isdt = item.StartDate.Ticks;
                var iedt = item.EndDate.Ticks;

                var cond1 = psdt > isdt && pedt < iedt;
                var cond2 = psdt < iedt && pedt > isdt; //NOT DONE
            });
            return true;
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
