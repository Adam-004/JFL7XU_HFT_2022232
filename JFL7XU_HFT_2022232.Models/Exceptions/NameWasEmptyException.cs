﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFL7XU_HFT_2022232.Models.Exceptions
{
    public class NameWasEmptyException : Exception
    {
        public NameWasEmptyException(){}
        public NameWasEmptyException(string message) : base(message){}
    }
}
