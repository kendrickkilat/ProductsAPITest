using Microsoft.EntityFrameworkCore;
using ProductsAPITest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPITest.Repositories
{
    public class PricingRepository:IRepository<Pricing, Guid>
    {
        private readonly ProductContext context;

        public PricingRepository(ProductContext context)
        {
            this.context = context;
        }

        public async Task<Pricing> Add(Pricing entity)
        {
            entity.id = Guid.NewGuid();
            await context.Pricings.AddAsync(entity);
            return entity;
        }

        public async Task<List<Pricing>> GetAll()
        {
            return await context.Pricings.ToListAsync();
        }

        public async Task<Pricing> GetById(Guid id)
        {
            return await context.Pricings.FindAsync(id);
        }

        public void Remove(Pricing entity)
        {
            context.Pricings.Remove(entity);
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public void Update(Pricing entity)
        {
            context.Pricings.Update(entity);
        }
    }
}
