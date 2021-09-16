using ProductsAPITest.Models;
using ProductsAPITest.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPITest.Services
{
    public class ProductService : IService<Product, Guid>
    {
        private readonly IRepository<Product, Guid> _productRepository;

        public ProductService(IRepository<Product, Guid> productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<string> Add(Product entity)
        {
            entity.Id = Guid.NewGuid();
            await _productRepository.Add(entity);
            await _productRepository.Save();
            return "Success";
        }

        public async Task<List<Product>> GetAll()
        {
            return await _productRepository.GetAll();
        }

        public async Task<Product> GetById(Guid id)
        {
            var product = await _productRepository.GetById(id);
            return product;
        }

        public async Task<string> Remove(Guid id)
        {
            var item = await _productRepository.GetById(id);
            if(item != null)
            {
                var res = await _productRepository.Remove(item);
                await _productRepository.Save();
                return "Success";
            }
            return "Error";
        }

        public async Task<string> Update(Guid id, Product entity)
        {
            var exist = await _productRepository.GetById(id);
            if(exist != null)
            {
                exist.Name = entity.Name; //This works instead of entity.id = exist.id for some reason (something about tracking purposes in entity framework)
                var res = await _productRepository.Update(exist);
                await _productRepository.Save();
                return "Success";
            }
            return "ID not Found";
        }
    }
}
