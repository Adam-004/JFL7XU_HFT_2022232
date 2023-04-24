using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFL7XU_HFT_2022232.Models.Exceptions
{
    public class GivenIDAlreadyExistsException : Exception
    {
        public GivenIDAlreadyExistsException(){}
        public GivenIDAlreadyExistsException(string message) : base(message){}
    }
}
