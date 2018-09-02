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

        public static Double ReLU(Double x)
		    => x < 0 ? 0 : x;

        public static double ReluDerivate(Double x)
            => x < 0 ? 0 : 1;

        static readonly Dictionary<FuncDD, FuncDD> derivates;
        static void initDerivates()
        {
            derivates[ReLU] = ReluDerivate;
            derivates[ReluDerivate] = ReLU;
        }
    }
}
