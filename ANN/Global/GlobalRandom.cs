using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global
{
    public sealed class GlobalRandom
    {
        public static Random Instance { get => instance; }

        public static double NextDouble(double minVal, double maxVal)
            => Instance.NextDouble(minVal, maxVal);

        public static double NextDouble()
            => Instance.NextDouble();

        public static double[] NextDoubleArr(int count)
            => Instance.NextDoubleArr(count);

        public static double[] NextDoubleArr(int count, double minVal, double maxVal)
            => Instance.NextDoubleArr(count, minVal, maxVal);
        
        private static readonly Random instance = new Random();
        private GlobalRandom() { }
    }
}
