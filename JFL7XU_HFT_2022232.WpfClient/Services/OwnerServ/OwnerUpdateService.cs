using JFL7XU_HFT_2022232.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFL7XU_HFT_2022232.WpfClient.Services.OwnerServ
{
    class OwnerUpdateService
    {
        public OwnerUpdateService() { }
        public Owner Update(Owner owner)
        {
            var updater = new OwnerUpdateWindow(owner);
            if (updater.ShowDialog() == true)
            {
                owner = updater.Updated;
            }
            return owner;
        }
    }
}
