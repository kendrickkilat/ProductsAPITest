using Microsoft.AspNetCore.Mvc;
using ProductsAPITest.Constants;
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _pricingService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _pricingService.GetById(id);
            if (result == null)
            {
                return NotFound($"Order with ID {id} was not found");
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PricingDto pricing)
        {
            var result =  await _pricingService.Add(pricing);
            var link = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}/{pricing.PricingId}";
            if (result != Messages.SUCCESS)
            {
                return NotFound("Pricing creation failed: Invalid StartDate or EndDate");
            }

            return Created(link, pricing);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Edit(Guid id, PricingDto pricing)
        {
            var result = await _pricingService.Update(id, pricing);
            if (result != Messages.SUCCESS)
            {
                return NotFound(result);
            }

            return Ok("Update Successful");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _pricingService.Remove(id);
            if (result != Messages.SUCCESS)
            {
                return NotFound($"Pricing with ID {id} was not found");
            }

            return Ok("Delete Successful");
        } 
    }
}
