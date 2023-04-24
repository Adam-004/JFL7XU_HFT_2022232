using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFL7XU_HFT_2022232.Models.Exceptions
{
    public class NoOwnerFoundWithGivenIdException : Exception
    {
        public NoOwnerFoundWithGivenIdException(){}
        public NoOwnerFoundWithGivenIdException(string message) : base(message){} 
    }
}
