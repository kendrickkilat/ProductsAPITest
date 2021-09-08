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
        public string Add(Product entity)
        {
            _productRepository.Add(entity);
            _productRepository.Save();
            return "Success";
        }

        public List<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public Product GetById(Guid id)
        {
            var product = _productRepository.GetById(id);
            return product;
        }

        public string Remove(Guid id)
        {
            var item = _productRepository.GetById(id);
            if(item != null)
            {
                _productRepository.Remove(item);
                _productRepository.Save();
                return "Success";
            }
            return "Error";
        }

        public string Update(Guid id, Product entity)
        {
            var exist = _productRepository.GetById(id);
            if(exist != null)
            {
                exist.Name = entity.Name; //This works instead of entity.id = exist.id for some reason (something about tracking purposes in entity framework)
                _productRepository.Update(exist);
                _productRepository.Save();
                return "Success";
            }
            return "ID not Found";
        }
    }
}
