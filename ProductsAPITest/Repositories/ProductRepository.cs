using Microsoft.EntityFrameworkCore;
using ProductsAPITest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPITest.Repositories
{
    public class ProductRepository : BaseRepository<Product, Guid>, IProductRepository
    {
        private readonly ProductContext context;
        public ProductRepository(ProductContext context):base(context)
        {
        }
    }
}
