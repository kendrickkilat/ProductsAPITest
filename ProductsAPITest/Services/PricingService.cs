using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductsAPITest.Constants;
using ProductsAPITest.Dtos;
using ProductsAPITest.Models;
using ProductsAPITest.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPITest.Services
{
    public class PricingService : IService<PricingDto, Guid>
    {
        private readonly IRepository<Pricing, Guid> _pricingRepository;
        private readonly IMapper mapper;

        public PricingService(IRepository<Pricing, Guid> pricingRepository, IMapper mapper)
        {
            _pricingRepository = pricingRepository;
            this.mapper = mapper;
        }
        public async Task<string> Add(PricingDto entityDto)
        {
            var isDateNotValid = await _pricingRepository.Entity().AnyAsync(p => entityDto.StartDate.Ticks <= p.EndDate.Ticks && p.StartDate.Ticks <= entityDto.EndDate.Ticks && p.PricingId != entityDto.PricingId);
            
            if(isDateNotValid)
            {
                return Messages.ERROR;
            }

            entityDto.PricingId = Guid.NewGuid();
            var entity = mapper.Map<Pricing>(entityDto);
            await _pricingRepository.Add(entity);
            await _pricingRepository.Save();

            return Messages.SUCCESS;
        }

        public async Task<List<PricingDto>> GetAll()
        {
            var result = await _pricingRepository.GetAll();
            var resultDto = mapper.Map<List<PricingDto>>(result);
            return resultDto;
        }

        public async Task<PricingDto> GetById(Guid id)
        {
            var pricing = await _pricingRepository.GetById(id);
            var pricingDto = mapper.Map<PricingDto>(pricing);
            return pricingDto;
        }

        public async Task<string> Remove(Guid id)
        {
            var pricing = await _pricingRepository.GetById(id);
            if(pricing == null)
            {
                return Messages.ERROR;
            }

            //var pricing = mapper.Map<Pricing>(pricingDto);
            await _pricingRepository.Remove(pricing);
            await _pricingRepository.Save();

            return Messages.SUCCESS;
        }

        public async Task<string> Update(Guid id, PricingDto entityDto)
        {
            var pricing = await _pricingRepository.GetById(id);

            var isDateNotValid = await _pricingRepository.Entity()
                 .AnyAsync(p => (pricing.StartDate.Ticks <= p.EndDate.Ticks && p.StartDate.Ticks <= pricing.EndDate.Ticks) && p.PricingId != pricing.PricingId);

            if(pricing == null)
            {
                return Messages.ERROR;
            }
            if (isDateNotValid)
            {
                return "Pricing Dates are not valid!";
            }
            
           
            var entity = mapper.Map<Pricing>(entityDto);

            pricing.StartDate = entity.StartDate;
            pricing.EndDate = entity.EndDate;
            pricing.ProductId = entity.ProductId;
            pricing.Price = entity.Price;

            await _pricingRepository.Update(pricing);
            await _pricingRepository.Save();
          
            return Messages.SUCCESS;
        }
    }
}
