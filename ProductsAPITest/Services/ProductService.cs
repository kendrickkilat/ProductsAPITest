using ProductsAPITest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPITest.Services
{
    public class ProductService : IProductService
    {

       private ProductContext _productContext;

       public ProductService(ProductContext productContext)
        {
            _productContext = productContext;
        }
        public Product AddProduct(Product product)
        {
             product.Id = Guid.NewGuid();
             _productContext.Products.Add(product);
             _productContext.SaveChanges();
             return product;
        }

        public void DeleteProduct(Product product)
        {
            _productContext.Products.Remove(product);
            _productContext.SaveChanges();
        }

        public Product EditProduct(Product product)
        {
            var existingProduct = _productContext.Products.Find(product.Id);
            if(existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                _productContext.Products.Update(existingProduct);
                _productContext.SaveChanges();
            }
            return product;
        }

        public Product GetProduct(Guid id)
        {
            var product = _productContext.Products.Find(id);
            return product;
            // throw new NotImplementedException();
        }

        public List<Product> GetProducts()
        {
            return _productContext.Products.ToList();
        }
    }
}
