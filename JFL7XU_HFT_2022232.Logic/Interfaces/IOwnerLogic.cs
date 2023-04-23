using JFL7XU_HFT_2022232.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFL7XU_HFT_2022232.Logic.Interfaces
{
    public interface IOwnerLogic
    {
        void Create(Owner item);
        void Update(Owner item);
        Owner Read(int id);
        IQueryable<Owner> ReadAll();
        void Delete(int id);
    }
}
