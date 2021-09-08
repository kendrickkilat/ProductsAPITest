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

        public Pricing Add(Pricing entity)
        {
            entity.id = Guid.NewGuid();
            context.Pricings.Add(entity);
            return entity;
        }

        public List<Pricing> GetAll()
        {
            return context.Pricings.ToList();
        }

        public Pricing GetById(Guid id)
        {
            return context.Pricings.Find(id);
        }

        public void Remove(Pricing entity)
        {
            var existingOrder = context.Pricings.Find(entity.id);
            if (existingOrder != null)
            {
                context.Pricings.Remove(entity);
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public Pricing Update(Pricing entity)
        {
            context.Pricings.Update(entity);
            return entity;
        }
    }
}
