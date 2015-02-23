using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaptorWebWalker.HelperClasses
{
    class Utilities
    {

        internal string SecondsToDHMS(double seconds)
        {
           return TimeSpan.FromSeconds(seconds).ToString();
        }

    }
}
