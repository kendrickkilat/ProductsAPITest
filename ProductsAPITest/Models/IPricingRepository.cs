using ProductsAPITest.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPITest.Models
{
    public interface IPricingRepository : IRepository<Pricing,Guid>
    {
    }
}
