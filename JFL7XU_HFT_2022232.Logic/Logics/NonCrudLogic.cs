using JFL7XU_HFT_2022232.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFL7XU_HFT_2022232.Logic.Logics
{
    public class NonCrudLogic
    {
        public IEnumerable<Starship> ListShips_WhichBuiltAfter(int year)
        {

        }
        public IEnumerable<Starship> ListShips_WhichBuiltBefore(int year)
        {

        }
        public IEnumerable<Hangar> ListHangars_WithShipsMoreThan(int quantity)
        {
            return repo.ReadAll().Where(h => h.Owner.Ships.Count > quantity);
        }
    }
}
