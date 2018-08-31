using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace ANN
{
    public class InputLayer : ANNLayer
    {
        public override int Size { get => size; }
        int size;

        public InputLayer(int size)
            => this.size = size;

        public override Matrix<double> feed(Matrix<double> input)
            => input;

        public override void feedForTrain(Matrix<double> input)
        { }
    }
}
