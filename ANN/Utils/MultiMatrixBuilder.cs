using ExtensionMethods;
using Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class MultiMatrixBuilder
    {
        public MultiMatrix random(int[] sizes)
        { 
            var m = new MultiMatrix();
            m.useData(GlobalRandom.NextDoubleArr(sizes.product()), sizes);
            return m;
        }

        public MultiMatrix repeat(int[] sizes, double value)
        {
            var m = new MultiMatrix();
            m.useData(ArrayBuilder.repeat(value, sizes.product()), sizes);
            return m;
        }
    }
}
