using JFL7XU_HFT_2022232.Models;
using JFL7XU_HFT_2022232.WpfClient.Services.Interfaces;
using JFL7XU_HFT_2022232.WpfClient.Services.OwnerServ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFL7XU_HFT_2022232.WpfClient.Services.StarshipServ
{
    class StarshipService : StarshipServiceInterface
    {
        public Starship Starship { get; set; } = new();
        public Starship Create()
        {
            var creator = new StarshipCreateWindow();
            Starship = new();
            if (creator.ShowDialog() == true)
            {
                Starship = creator.starship;
            }
            return Starship;
        }
        public Starship Update(Starship ship)
        {
            Starship = new();
            var updater = new StarshipUpdateWindow(ship);
            if (updater.ShowDialog() == true)
            {
                Starship = updater.Updated;
            }
            return Starship;
        }
    }
}
