using JFL7XU_HFT_2022232.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFL7XU_HFT_2022232.Logic.Interfaces
{
    public interface IStarshipLogic
    {
        void Create(Starship item);
        void Update(Starship item);
        Starship Read(int id);
        IEnumerable<Starship> ReadAll();
        void Delete(int id);
    }
}
