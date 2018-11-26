using ExtensionMethods;
using GlobalRandom_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace MultiMatrix_
{
    public class MultiMatrixBuilder
    {
        public MultiMatrix useData(double[] data, int[] dimensions)
        {
            var matrix = new MultiMatrix();
            matrix.use(data, dimensions);
            return matrix;
        }

        public MultiMatrix random(int[] sizes)
            => this.useData(GlobalRandom.NextDoubleArr(sizes.product()), sizes);

        public MultiMatrix random(int[] sizes, double minVal, double maxVal)
            => this.useData(GlobalRandom.NextDoubleArr(sizes.product(), minVal, maxVal), sizes);

        public MultiMatrix repeat(int[] sizes, double value)
            => this.useData(ArrayBuilder.repeat(value, sizes.product()), sizes);
    }
}
