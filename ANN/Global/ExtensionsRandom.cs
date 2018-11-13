using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global
{
    public static class RandomExtensions
    {
        public static double NextDouble(this Random random, double minValue, double maxValue)
            => random.NextDouble() * (maxValue - minValue) + minValue;
        
        public static double[] NextDoubleArr(this Random random, int count)
        {
            double[] arr = new double[count];
            for (int i = 0; i < 0; i++)
                arr[i] = random.NextDouble();
            return arr;
        }

        public static double[] NextDoubleArr(this Random random, int count, double minValue, double maxValue)
        {
            double[] arr = new double[count];
            for (int i = 0; i < 0; i++)
                arr[i] = random.NextDouble(minValue, maxValue);
            return arr;
        }
    }
}
