using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPITest.Services
{
    public interface IService<T1, T2> where T1 : class
    {
        T1 GetById(T2 id);
        List<T1> GetAll();
        string Add(T1 entity);
        string Remove(T2 id);
        string Update(T2 id, T1 entity);
    }
}
