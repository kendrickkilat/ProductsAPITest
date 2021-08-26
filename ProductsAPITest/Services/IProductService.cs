using ProductsAPITest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPITest.Services
{
    public interface IProductService
    {
        List<Product> GetAll();
        Product GetById(Guid id);
        Product Add(Product product);
        void Delete(Product product);
        Product Edit(Product product);
    }
}
