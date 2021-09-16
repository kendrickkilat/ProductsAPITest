using Microsoft.EntityFrameworkCore;
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
            var result = await _pricingRepository.Entity().Where(p => entity.StartDate.Ticks <= p.EndDate.Ticks && p.StartDate.Ticks <= entity.EndDate.Ticks ).ToListAsync();
            if(result.Count == 0)
            {
                entity.id = Guid.NewGuid();
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
                var res = await _pricingRepository.Remove(pricing);
                await _pricingRepository.Save();
                return "Success";
            }
            return "Error";
        }

        public async Task<string> Update(Guid id, Pricing entity)
        {
            //When updating, api should be able to update price even if  compared dates are the same
            var exist = await _pricingRepository.GetById(id);
            var result = await _pricingRepository.Entity().Where(p => exist.StartDate.Ticks < p.EndDate.Ticks && p.StartDate.Ticks < exist.EndDate.Ticks).ToListAsync();

            if (exist != null)
            {
                var cond1 = exist.StartDate.Ticks == entity.StartDate.Ticks && exist.EndDate.Ticks == entity.EndDate.Ticks;
                var cond2 = exist.Price != entity.Price;
                if (result.Count == 0 || (cond1 && cond2))
                {
                    exist.StartDate = entity.StartDate;
                    exist.EndDate = entity.EndDate;
                    exist.ProductId = entity.ProductId;
                    exist.Price = entity.Price;

                    var res = await _pricingRepository.Update(entity);
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
