using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPITest.Services
{
    public interface IService<T1, T2> where T1 : class
    {
        Task<T1> GetById(T2 id);
        Task<List<T1>> GetAll();
        Task<string> Add(T1 entity);
        Task<string> Remove(T2 id);
        Task<string> Update(T2 id, T1 entity);
    }
}
