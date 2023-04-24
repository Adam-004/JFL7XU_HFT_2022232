using JFL7XU_HFT_2022232.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFL7XU_HFT_2022232.Logic.Interfaces
{
    public interface INonCrudLogic
    {
        IEnumerable<Starship> ListShips_WhichBuiltAfter(int year);
        IEnumerable<Hangar> ListHangars_WithShipsMoreThan(int quantity);
        IEnumerable<Hangar> ListHangars_WithShipsLessThan(int quantity);
        IEnumerable<Owner> ListOwners_OlderThan(int year);
        IEnumerable<Owner> ListOwners_YoungerThan(int year);
        IEnumerable<Owner> ListOwners_YoungerAndHasMoreShipsThan(int year, int quantity);
    }
}
