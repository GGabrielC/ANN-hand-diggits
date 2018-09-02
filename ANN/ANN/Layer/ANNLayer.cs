using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatrixD = MathNet.Numerics.LinearAlgebra.Matrix<System.Double>;

namespace Layers
{
    public abstract class ANNLayer
    {
        public abstract int Size { get; }
        public abstract MatrixD feed(MatrixD input);
        public abstract void feedForTrain(MatrixD input);
        public abstract void backPropagate(MatrixD partialDerivate);
    }
}
