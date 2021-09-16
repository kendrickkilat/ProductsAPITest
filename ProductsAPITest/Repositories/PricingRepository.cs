using Microsoft.EntityFrameworkCore;
using ProductsAPITest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPITest.Repositories
{
    public class PricingRepository: BaseRepository<Pricing, Guid>, IPricingRepository
    {
        private readonly ProductContext context;

        public PricingRepository(ProductContext context):base(context)
        {
        }
    }
}
