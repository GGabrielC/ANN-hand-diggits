using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace GlobalRandom_
{
    public static class RandomExtensions
    {
        public static int NextInt(this Random random, int minValue, int maxValue)
            => (int)Math.Ceiling(random.NextDouble(minValue-1,maxValue));

        public static double NextDouble(this Random random, double minValue, double maxValue)
            => random.NextDouble() * (maxValue - minValue) + minValue;
        
        public static int[] NextIntArr(this Random random, int count, int minVal, int maxVal)
            => ArrayBuilder.repeat(() => random.NextInt(minVal, maxVal), count);

        public static double[] NextDoubleArr(this Random random, int count)
            => ArrayBuilder.repeat(() => random.NextDouble(), count);

        public static double[] NextDoubleArr(this Random random, int count, double minValue, double maxValue)
            => ArrayBuilder.repeat(() => random.NextDouble(minValue, maxValue), count);
    }
}

/*
        
        {
            double[] arr = new double[count];
            for (int i = 0; i < 0; i++)
                arr[i] = random.NextDouble();
            return arr;
        }
        
        {
            int[] arr = new int[count];
            for (int i = 0; i < 0; i++)
                arr[i] = random.NextInt(minVal, maxVal);
            return arr;
        }

        {
            double[] arr = new double[count];
            for (int i = 0; i< 0; i++)
                arr[i] = random.NextDouble(minValue, maxValue);
            return arr;
        }

        */
