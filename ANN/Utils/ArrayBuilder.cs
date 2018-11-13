using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class ArrayBuilder
    {
        public static T[] repeat<T>(T value, int times)
        {
            var arr = new T[times];
            for (int i = 0; i < arr.Length; i++)
                arr[i] = value;
            return arr;
        }
    }
}
