﻿using JFL7XU_HFT_2022232.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFL7XU_HFT_2022232.WpfClient.Services.Interfaces
{
    interface OwnerServiceInterface
    {
        public Owner Owner { get; set; }
        public Owner Create();
        public Owner Update(Owner owner);
    }
}
