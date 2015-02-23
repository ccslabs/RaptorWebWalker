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

        internal string GetString(byte[] data)
        {
            return Encoding.UTF8.GetString(data, 0, data.Length);
        }

        internal byte[] GetBytes(string data)
        {
            return Encoding.UTF8.GetBytes(data);
        }

    }
}
