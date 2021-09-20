using Microsoft.AspNetCore.Mvc;
using ProductsAPITest.Dtos;
using ProductsAPITest.Models;
using ProductsAPITest.Repositories;
using ProductsAPITest.Services;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductsAPITest.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PricingController : ControllerBase
    {
        private readonly IService<PricingDto, Guid> _pricingService;

        public PricingController(IService<PricingDto, Guid> pricingService)
        {
            _pricingService = pricingService;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _pricingService.GetAll());
        }

        // GET api/<ValuesController>/5
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _pricingService.GetById(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound($"Order with ID {id} was not found");
        }
        // Checks if price start date and end date is valid
        [HttpPost]
        public async Task<IActionResult> Create(PricingDto pricing)
        {
            var result =  await _pricingService.Add(pricing);
            var link = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}/{pricing.id}";
            if (result == "Success")
            {
                return Created(link, pricing);
            }
            return NotFound("Pricing creation failed: Invalid StartDate or EndDate");
        }

        // PUT api/<ValuesController>/5
        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> Edit(Guid id, PricingDto pricing)
        {
            var result = await _pricingService.Update(id, pricing);
            if (result == "Success")
            {
                return Ok("Update Successful");
            }
            return NotFound(result);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _pricingService.Remove(id);
            if (result == "Success")
            {
                return Ok("Delete Successful");
            }
            return NotFound($"Pricing with ID {id} was not found");
        } 
    }
}
