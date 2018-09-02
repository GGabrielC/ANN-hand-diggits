using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MatrixD = MathNet.Numerics.LinearAlgebra.Matrix<System.Double>;

namespace Layers
{
    public class InputLayer : ANNLayer
    {
        public override int Size { get => size; }
        int size;
        MatrixD input;

        public InputLayer(int size)
            => this.size = size;

        public override MatrixD feed(MatrixD input)
            => input;

        public override void feedForTrain(MatrixD input)
            => this.input = input;

        public override void backPropagate(MatrixD partialDerivate)
        { }
    }
}
