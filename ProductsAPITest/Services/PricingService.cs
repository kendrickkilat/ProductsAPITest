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
        public string Add(Pricing entity)
        {
            var isValid = true;
            var pricings = _pricingRepository.GetAll();
            foreach(var item in pricings)
            {
                if(entity.StartDate.Ticks <= item.EndDate.Ticks && item.StartDate.Ticks <= entity.EndDate.Ticks)
                {
                    isValid = false;
                }
            }
            if (isValid)
            {
                _pricingRepository.Add(entity);
                _pricingRepository.Save();
                return "Success";
            }
            return "Error";
        }

        public List<Pricing> GetAll()
        {
            return _pricingRepository.GetAll();
        }

        public Pricing GetById(Guid id)
        {
            var pricing = _pricingRepository.GetById(id);
            return pricing;
        }

        public string Remove(Guid id)
        {
            var pricing = _pricingRepository.GetById(id);
            if(pricing != null)
            {
                _pricingRepository.Remove(pricing);
                _pricingRepository.Save();
                return "Success";
            }
            return "Error";
        }

        public string Update(Guid id, Pricing entity)
        {
            var exist = _pricingRepository.GetById(id);
            var pricings = _pricingRepository.GetAll();
            
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
