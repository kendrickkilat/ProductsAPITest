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
            var result = await _pricingRepository.Entity().Where(p => entityDto.StartDate.Ticks <= p.EndDate.Ticks && p.StartDate.Ticks <= entityDto.EndDate.Ticks && p.id != entityDto.id ).AnyAsync();
            if(!result)
            {
                entityDto.id = Guid.NewGuid();
                var entity = mapper.Map<Pricing>(entityDto);
                await _pricingRepository.Add(entity);
                await _pricingRepository.Save();
                return Messages.Success;
            }
            else {
                return Messages.Error;
            }
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
            if(pricing != null)
            {
                //var pricing = mapper.Map<Pricing>(pricingDto);
                await _pricingRepository.Remove(pricing);
                await _pricingRepository.Save();
                return Messages.Success;
            }
            else
            {
                return Messages.Error;
            }
        }

        public async Task<string> Update(Guid id, PricingDto entityDto)
        {
            var pricing = await _pricingRepository.GetById(id);

            var result = await _pricingRepository.Entity().Where(p =>
                (pricing.StartDate.Ticks <= p.EndDate.Ticks && p.StartDate.Ticks <= pricing.EndDate.Ticks) && p.id != pricing.id)
                 .AnyAsync();

            if (pricing != null)
            {
                if (!result)
                {
                    var entity = mapper.Map<Pricing>(entityDto);

                    //entity.id = pricing.id; //mugana ni siya pero sa orderService na code di mugana
                    pricing.StartDate = entity.StartDate;
                    pricing.EndDate = entity.EndDate;
                    pricing.ProductId = entity.ProductId;
                    pricing.Price = entity.Price;

                    await _pricingRepository.Update(pricing);
                    await _pricingRepository.Save();
                    return Messages.Success;
                }
                else
                {
                    return "Pricing Dates are not valid";
                }
            }
            else
            {
                return Messages.Error;
            }
        }
    }
}
