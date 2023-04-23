using JFL7XU_HFT_2022232.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace JFL7XU_HFT_2022232.Logic.Interfaces
{
    public interface IHangarLogic
    {
        void Create(Hangar item);
        void Update(Hangar item);
        Hangar Read(int id);
        IQueryable<Hangar> ReadAll();
        void Delete(int id);
    }
}
