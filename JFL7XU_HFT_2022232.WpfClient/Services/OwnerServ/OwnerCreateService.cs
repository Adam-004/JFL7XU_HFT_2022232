using JFL7XU_HFT_2022232.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFL7XU_HFT_2022232.WpfClient.Services.OwnerServ
{
    class OwnerCreateService
    {
        public OwnerCreateService() { }
        public Owner Create()
        {
            var creator = new OwnerCreateWindow();
            var created = new Owner();
            if (creator.ShowDialog() == true)
            {
                created = creator.owner;
            }
            return created;
        }
    }
}
