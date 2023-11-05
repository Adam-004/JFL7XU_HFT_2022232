using JFL7XU_HFT_2022232.Models;
using JFL7XU_HFT_2022232.WpfClient.Services.HangarServ;
using JFL7XU_HFT_2022232.WpfClient.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFL7XU_HFT_2022232.WpfClient.Services.OwnerServ
{
    class OwnerService : OwnerServiceInterface
    {
        public Owner Owner { get; set; } = new();
        public Owner Create()
        {
            var creator = new OwnerCreateWindow();
            Owner = new();
            if (creator.ShowDialog() == true)
            {
                Owner = creator.owner;
            }
            return Owner;
        }
        public Owner Update(Owner owner)
        {
            Owner = new();
            var updater = new OwnerUpdateWindow(owner);
            if (updater.ShowDialog() == true)
            {
                Owner = updater.Updated;
            }
            return Owner;
        }
    }
}
