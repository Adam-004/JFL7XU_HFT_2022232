using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFL7XU_HFT_2022232.Models
{
    public class OwnershipStatistics
    {
        public OwnershipStatistics(Owner owner, Hangar hangar, IEnumerable<Starship> ships)
        {
            OwnerName = owner.Name;
            OwnerAge = owner.Age;
            HangarName = hangar.Name;
            HangarLocation = hangar.Location;
            Ships = ships;
        }

        string OwnerName;
        int OwnerAge;
        string HangarName;
        string HangarLocation;
        public IEnumerable<Starship> Ships { get; set; }

        public override string ToString()
        {
            string Out = "";
            Out += "Owner:\n" + OwnerName + " " + OwnerAge + " years old, has hangar at " + HangarLocation + ", named " + HangarName + ".\nOwner's ships:\n";
            int i=0;
            foreach (var ship in Ships)
            {
                i++;
                Out += "\n\t"+i +". ship: '"+ship.Name+"' type: "+((ShipType)ship.Type).ToString()+", built in "+ship.YearOfManu+".";
            }
            Out += "\n";
            return Out;
        }
    }
}
