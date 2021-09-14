using ProductsAPITest.Models;
using ProductsAPITest.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPITest.Services
{
    public class PricingService : IService<Pricing, Guid>
    {
        private readonly IRepository<Pricing, Guid> _pricingRepository;

        public PricingService(IRepository<Pricing, Guid> pricingRepository)
        {
            _pricingRepository = pricingRepository;
        }
        public async Task<string> Add(Pricing entity)
        {
            var isValid = true;
            var pricings = await _pricingRepository.GetAll();
            foreach(var item in pricings)
            {
                if(entity.StartDate.Ticks <= item.EndDate.Ticks && item.StartDate.Ticks <= entity.EndDate.Ticks)
                {
                    isValid = false;
                }
            }
            if (isValid)
            {
               await _pricingRepository.Add(entity);
               await _pricingRepository.Save();
                return "Success";
            }
            return "Error";
        }

        public async Task<List<Pricing>> GetAll()
        {
            return await _pricingRepository.GetAll();
        }

        public async Task<Pricing> GetById(Guid id)
        {
            var pricing = await _pricingRepository.GetById(id);
            return pricing;
        }

        public async Task<string> Remove(Guid id)
        {
            var pricing = await _pricingRepository.GetById(id);
            if(pricing != null)
            {
                _pricingRepository.Remove(pricing);
                await _pricingRepository.Save();
                return "Success";
            }
            return "Error";
        }

        public async Task<string> Update(Guid id, Pricing entity)
        {
            var exist = await _pricingRepository.GetById(id);
            var pricings = await _pricingRepository.GetAll();
            
            if(exist != null)
            {
                var isValid = true;
                foreach (var item in pricings)
                {
                    if (entity.StartDate.Ticks <= item.EndDate.Ticks && item.StartDate.Ticks <= entity.EndDate.Ticks)
                    {
                        isValid = false;
                    }
                }

                if (isValid)
                {
                    exist.StartDate = entity.StartDate;
                    exist.EndDate = entity.EndDate;
                    exist.ProductId = entity.ProductId;
                    exist.Price = entity.Price;

                    _pricingRepository.Update(entity);
                    await _pricingRepository.Save();
                    return "Success";
                }
                else
                {
                    return "Pricing Dates are not valid";
                }
               
            }
            return "ID Not Found";
        }
    }
}
