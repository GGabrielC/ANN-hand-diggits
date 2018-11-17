using ExtensionMethods;
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
        private static readonly Random instance = new Random();

        public static int Next()
            => Instance.Next();

        public static int NextInt(int minValue, int maxValue)
            => Instance.NextInt(minValue, maxValue);

        public static double NextDouble()
            => Instance.NextDouble();

        public static double NextDouble(double minVal, double maxVal)
            => Instance.NextDouble(minVal, maxVal);

        public static int[] NextIntArr(int count, int minVal, int maxVal)
            => Instance.NextIntArr(count, minVal, maxVal);

        public static double[] NextDoubleArr(int count)
            => Instance.NextDoubleArr(count);

        public static double[] NextDoubleArr(int count, double minVal, double maxVal)
            => Instance.NextDoubleArr(count, minVal, maxVal);

        public static void SetRandomNonNegative(int[] data)
            => data.applyFunc(() => Instance.Next());

        public static void SetRandom(int[] data, int minVal, int maxVal)
            => data.applyFunc(() => Instance.NextInt(minVal, maxVal));

        public static void SetRandom(double[] data)
            => data.applyFunc(() => Instance.NextDouble());

        public static void SetRandom(double[] data, double minVal, double maxVal)
            => data.applyFunc(() => Instance.NextDouble(minVal, maxVal));

        private GlobalRandom() { }
        
    }
}
