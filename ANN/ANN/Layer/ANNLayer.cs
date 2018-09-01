using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers
{
    public abstract class ANNLayer
    {
        public abstract int Size { get; }
        public abstract Matrix<Double> feed(Matrix<double> input);
        public abstract void feedForTrain(Matrix<double> input);
        
    }
}
