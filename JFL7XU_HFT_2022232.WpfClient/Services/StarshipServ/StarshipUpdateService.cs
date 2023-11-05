using JFL7XU_HFT_2022232.Models;
using JFL7XU_HFT_2022232.WpfClient.Services.OwnerServ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFL7XU_HFT_2022232.WpfClient.Services.StarshipServ
{
    class StarshipUpdateService
    {
        public StarshipUpdateService() { }
        public Starship Update(Starship ship)
        {
            var updater = new StarshipUpdateWindow(ship);
            if (updater.ShowDialog() == true)
            {
                ship = updater.Updated;
            }
            return ship;
        }
    }
}
