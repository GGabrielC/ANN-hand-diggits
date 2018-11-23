using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FuncDD = System.Func<System.Double, System.Double>;

namespace Utils
{
    public class Functions
    {
        public static FuncDD getDerivate(FuncDD function)
            => derivates[function];

        public static FuncDD[] getDerivates(FuncDD[] functions)
            => functions.Select( function => getDerivate(function) ).ToArray() ;

        public static Double ReLU(Double x)
		    => x < 0 ? 0 : x;
        public static double ReluDerivate(Double x)
            => x < 0 ? 0 : 1;

        public static double LeakyReLU(double x)
            => x < 0 ? 0.01*x : x;
        public static double LeakyReLUDerivate(double x)
            => x < 0 ? 0.01 : 1;

        public static double xOverModXplus1(double x)
            => x / (x + Math.Abs(x));
        public static double xOverModXplus1Derivate(double x)
            => Math.Pow(1 / (x + Math.Abs(x)), 2);

        public static double sigmoid(double x)
            => 1 / (Math.Pow(Math.E , -x) + 1);
        public static double sigmoidDerivate(double x)
            => Math.Pow(Math.E, -x) / Math.Pow(Math.Pow(Math.E, -x) + 1, 2);

        public static double tanh(double x)
            => Math.Tanh(x);
        public static double tanhDerivate(double x)
            => Math.Pow(1 / Math.Cosh(x), 2);

        // https://en.wikipedia.org/wiki/File:Gjl-t(x).svg

        protected static readonly Dictionary<FuncDD, FuncDD> derivates = new Dictionary<FuncDD, FuncDD>();
        static Functions()
        {
            derivates.Add(ReLU, ReluDerivate);
            derivates.Add(ReluDerivate, ReLU);

            derivates.Add(LeakyReLU, LeakyReLUDerivate);
            derivates.Add(LeakyReLUDerivate, LeakyReLU);

            derivates.Add(xOverModXplus1, xOverModXplus1Derivate);
            derivates.Add(xOverModXplus1Derivate, xOverModXplus1);

            derivates.Add(sigmoid, sigmoidDerivate);
            derivates.Add(sigmoidDerivate, sigmoid);

            derivates.Add(tanh, tanhDerivate);
            derivates.Add(tanhDerivate, tanh);
        }
    }
}
