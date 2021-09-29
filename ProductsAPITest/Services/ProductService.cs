using AutoMapper;
using ProductsAPITest.Dtos;
using ProductsAPITest.Models;
using ProductsAPITest.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductsAPITest.Constants;


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
            entity.ProductId = Guid.NewGuid();
            await _productRepository.Add(entity);
            await _productRepository.Save();
            return Messages.SUCCESS;
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
            var item = await _productRepository.GetById(id);
            if(item == null)
            {
                return Messages.ERROR;
            }
            //var item = mapper.Map<Product>(itemDto);
            await _productRepository.Remove(item);
            await _productRepository.Save();

            return Messages.SUCCESS;
        }

        public async Task<string> Update(Guid id, ProductDto entityDto)
        {
            var product = await _productRepository.GetById(id);
            if(product == null)
            {
                return Messages.ERROR;
            }
            var entity = mapper.Map<Product>(entityDto);
            product.Name = entity.Name;
            await _productRepository.Update(product);
            await _productRepository.Save();

            return Messages.SUCCESS;
        }
    }
}
