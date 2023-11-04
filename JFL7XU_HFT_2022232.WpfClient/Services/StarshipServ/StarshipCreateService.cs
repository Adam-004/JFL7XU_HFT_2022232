using JFL7XU_HFT_2022232.Models;
using JFL7XU_HFT_2022232.WpfClient.Services.OwnerServ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFL7XU_HFT_2022232.WpfClient.Services.StarshipServ
{
    class StarshipCreateService
    {
        public StarshipCreateService() { }
        public Starship Create()
        {
            var creator = new StarshipCreateWindow();
            var created = new Starship();
            if (creator.ShowDialog() == true)
            {
                created = creator.starship;
            }
            return created;
        }
    }
}
