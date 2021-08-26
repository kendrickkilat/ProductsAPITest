using ProductsAPITest.Models;
using ProductsAPITest.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPITest.Services
{
    public class ProductService2 : IProductService
    {

        //private ProductContext productRepository;
        private readonly IRepository<Product, Guid> productRepository;

        public ProductService2(IRepository<Product, Guid> productRepository)
        {
            this.productRepository = productRepository;
            //productRepository = productContext;
        }
        public Product Add(Product product)
        {
            //product.Id = Guid.NewGuid();
            productRepository.Add(product);
            productRepository.Save();
            return product;
        }

        public void Delete(Product product)
        {
            productRepository.Remove(product);
            productRepository.Save();
        }

        public Product Edit(Product product)
        {
            var existingProduct = productRepository.GetById(product.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                productRepository.Update(existingProduct);
                productRepository.Save();
            }
            return product;
        }

        public Product GetById(Guid id)
        {
            var product = productRepository.GetById(id);
            return product;
        }

        public List<Product> GetAll()
        {
            return productRepository.GetAll();
        }
    }
}
