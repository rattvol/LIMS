using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LIMS.Environment
{
    public static class DoubleTrim
    {
        public static string TrimIt(decimal item)
        {
            string temp = item.ToString();
            while (temp.EndsWith('0'))
            {
                temp = temp.TrimEnd('0');
            }
            if (temp.EndsWith(','))
            {
                temp = temp.TrimEnd(',');
            }
            return temp;
        }
    }
}
