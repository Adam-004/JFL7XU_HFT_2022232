using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFL7XU_HFT_2022232.Models.Exceptions
{
    public class NoStarshipFoundWithGivenIdException : Exception
    {
        public NoStarshipFoundWithGivenIdException(){}
        public NoStarshipFoundWithGivenIdException(string message) : base(message){}
    }
}
