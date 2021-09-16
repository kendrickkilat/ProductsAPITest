using Microsoft.EntityFrameworkCore;
using ProductsAPITest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPITest.Repositories
{
    public interface IRepository<T1, T2> where T1 : class
    {
        Task<T1> GetById(T2 id);
        Task<List<T1>> GetAll();

        Task<T1> Add(T1 entity);
        Task<bool> Remove(T1 entity);
        Task<bool> Update(T1 entity);
        Task Save();
        DbSet<T1> Entity();
    }
}
