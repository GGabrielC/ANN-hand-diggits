using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethods
{
    public static class NumberExtensions
    {
        public static bool EEquals(this double d1, double d2, double epsilon = 0.000001)
        {
            if (Math.Abs(d1 - d2) > epsilon)
                return false;
            return true;
        }

        public static int Abs(this int num)
        {
            if (num < 0)
                return -num;
            return num;
        }
        
    }
}
