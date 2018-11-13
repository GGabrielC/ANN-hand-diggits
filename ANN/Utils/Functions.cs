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

        protected static readonly Dictionary<FuncDD, FuncDD> derivates = new Dictionary<FuncDD, FuncDD>();
        static Functions()
        {
            derivates.Add(ReLU, ReluDerivate);
            derivates.Add(ReluDerivate, ReLU);
        }
    }
}
