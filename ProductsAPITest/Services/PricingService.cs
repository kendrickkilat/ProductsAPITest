using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
            var result = await _pricingRepository.Entity().Where(p => entityDto.StartDate.Ticks <= p.EndDate.Ticks && p.StartDate.Ticks <= entityDto.EndDate.Ticks && p.id != entityDto.id ).ToListAsync();
            if(result.Count == 0)
            {
                entityDto.id = Guid.NewGuid();
                var entity = mapper.Map<Pricing>(entityDto);
                await _pricingRepository.Add(entity);
                await _pricingRepository.Save();
                return "Success";
            }
            return "Error";
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
            var pricingDto = await this.GetById(id);
            if(pricingDto != null)
            {
                var pricing = mapper.Map<Pricing>(pricingDto);
                await _pricingRepository.Remove(pricing);
                await _pricingRepository.Save();
                return "Success";
            }
            return "Error";
        }

        public async Task<string> Update(Guid id, PricingDto entityDto)
        {
            // var existDto = await this.GetById(id);
            var exist = await _pricingRepository.GetById(id);
            
            var result = await _pricingRepository.Entity().Where(p => 
                (existDto.StartDate.Ticks <= p.EndDate.Ticks && p.StartDate.Ticks <= existDto.EndDate.Ticks) && p.id != existDto.id )
                 .ToListAsync()

                 //.AsNoTracking(); Try this <--

            if (existDto != null)
            {
                if (result.Count == 0)
                {
                    // var exist = mapper.Map<Pricing>(existDto);
                    var entity = mapper.Map<Pricing>(entityDto);

                    // entity.id = exist.id;//?????
                    exist.StartDate = entity.StartDate;
                    exist.EndDate = entity.EndDate;
                    exist.ProductId = entity.ProductId;
                    exist.Price = entity.Price;

                    entity.

                    await _pricingRepository.Update(exist);
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
