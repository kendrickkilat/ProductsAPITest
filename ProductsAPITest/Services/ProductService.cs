using AutoMapper;
using ProductsAPITest.Dtos;
using ProductsAPITest.Models;
using ProductsAPITest.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPITest.Services
{
    public class ProductService : IService<ProductDto, Guid>
    {
        private readonly IRepository<Product, Guid> _productRepository;
        private readonly IMapper mapper;

        public ProductService(IRepository<Product, Guid> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            this.mapper = mapper;
        }
        public async Task<string> Add(ProductDto entityDto)
        {
            var entity = mapper.Map<Product>(entityDto);
            entity.Id = Guid.NewGuid();
            await _productRepository.Add(entity);
            await _productRepository.Save();
            return "Success";
        }

        public async Task<List<ProductDto>> GetAll()
        {
            var products = await _productRepository.GetAll();
            var result = mapper.Map<List<ProductDto>>(products);
            return result;
        }

        public async Task<ProductDto> GetById(Guid id)
        {
            var product = await _productRepository.GetById(id);
            var result = mapper.Map<ProductDto>(product);
            return result;
        }

        public async Task<string> Remove(Guid id)
        {
            var itemDto = await this.GetById(id);
            if(itemDto != null)
            {
                var item = mapper.Map<Product>(itemDto);
                await _productRepository.Remove(item);
                await _productRepository.Save();
                return "Success";
            }
            return "Error";
        }

        public async Task<string> Update(Guid id, ProductDto entityDto)
        {
            var exist = await _productRepository.GetById(id);
            if(exist != null)
            {
                var entity = mapper.Map<Product>(entityDto);
                exist.Name = entity.Name; //This works instead of entity.id = exist.id for some reason (something about tracking purposes in entity framework)
                await _productRepository.Update(exist);
                await _productRepository.Save();
                return "Success";
            }
            return "ID not Found";
        }
    }
}
