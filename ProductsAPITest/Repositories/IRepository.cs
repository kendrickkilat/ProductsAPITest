using ProductsAPITest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPITest.Repositories
{
    public interface IRepository<T1, T2> where T1 : class
    {
        T1 GetById(T2 id);
        List<T1> GetAll();

        T1 Add(T1 entity);
        void Remove(T1 entity);
        T1 Update(T1 entity);
        void Save();
    }
}
