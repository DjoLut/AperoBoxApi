using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AperoBoxApi.Exceptions
{
    public class PersonnalException : Exception
    {
        public PersonnalException(string message)
            :base(message)
        { }
    }
}
