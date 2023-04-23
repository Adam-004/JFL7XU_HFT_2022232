using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFL7XU_HFT_2022232.Repository.Interfaces
{
    interface IRepository<T> where T : class
    {
        //CRUD
        void Create(T item);
        IQueryable<T> ReadAll();
        T Read(int id);
        void Update(T item);
        void Delete(int id);
    }
}
