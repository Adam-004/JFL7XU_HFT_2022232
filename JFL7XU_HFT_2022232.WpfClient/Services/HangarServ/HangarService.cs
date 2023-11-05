using JFL7XU_HFT_2022232.Models;
using JFL7XU_HFT_2022232.WpfClient.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFL7XU_HFT_2022232.WpfClient.Services.HangarServ
{
    class HangarService : HangarServiceInterface
    {
        public Hangar Hangar { get; set; } = new();
        public Hangar Create()
        {
            var creator = new HangarCreateWindow();
            Hangar = new();
            if (creator.ShowDialog() == true)
            {
                Hangar = creator.hangar;
            }
            return Hangar;
        }
        public Hangar Update(Hangar hangar)
        {
            Hangar = new();
            var updater = new HangarUpdateWindow(hangar);
            if (updater.ShowDialog() == true)
            {
                Hangar = updater.Updated;
            }
            return Hangar;
        }
    }
}
