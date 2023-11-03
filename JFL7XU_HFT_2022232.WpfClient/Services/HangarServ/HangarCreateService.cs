using JFL7XU_HFT_2022232.Models;
using JFL7XU_HFT_2022232.WpfClient.Services.OwnerServ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFL7XU_HFT_2022232.WpfClient.Services.HangarServ
{
    class HangarCreateService
    {
        public HangarCreateService() { }
        public Hangar Create()
        {
            var creator = new HangarCreateWindow();
            var created = new Hangar();
            if (creator.ShowDialog() == true)
            {
                created = creator.hangar;
            }
            return created;
        }
    }
}
