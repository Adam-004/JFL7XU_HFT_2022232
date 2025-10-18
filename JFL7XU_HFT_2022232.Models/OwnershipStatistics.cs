using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JFL7XU_HFT_2022232.Models
{
    public class OwnershipStatistics
    {
        public Owner Owner { get; set; }

        public Hangar Hangar { get; set; }

        public IEnumerable<Starship> ships { get; set; }

        public OwnershipStatistics(Owner owner, Hangar hangar, IEnumerable<Starship> ships)
        {
            Owner = owner;
            Hangar = hangar;
            this.ships = ships;
        }

        public override string ToString()
        {
            string Out = "";
            if (Owner is not null)
            {
                Out += "Owner:\n" + Owner.Name + " " + Owner.Age + " years old, has hangar at " + Hangar.Location + ", named " + Hangar.Name + ".\nOwner's ships:\n";
            }
            else { Out += "Deleted Owner.\nOwner's ships:\n"; }
            
            int i=0;
            foreach (var ship in ships)
            {
                i++;
                Out += "\n\t"+i +". ship: '"+ship.Name+"' type: "+((ShipType)ship.Type).ToString()+", built in "+ship.YearOfManu+".";
            }
            Out += "\n";
            return Out;
        }
    }
}
