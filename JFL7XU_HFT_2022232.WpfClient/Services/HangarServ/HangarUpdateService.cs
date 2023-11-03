using JFL7XU_HFT_2022232.Models;
using JFL7XU_HFT_2022232.WpfClient.Services.OwnerServ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFL7XU_HFT_2022232.WpfClient.Services.HangarServ
{
    class HangarUpdateService
    {
        public HangarUpdateService() { }
        public Hangar Update(Hangar hangar)
        {
            var updater = new HangarUpdateWindow(hangar);
            if (updater.ShowDialog() == true)
            {
                hangar = updater.Updated;
            }
            return hangar;
        }
    }
}
