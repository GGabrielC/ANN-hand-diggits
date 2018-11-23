using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Layers;
using MatrixD = MathNet.Numerics.LinearAlgebra.Matrix<System.Double>;

namespace ANN
{
    public interface Trainer
    {
        void train(MatrixD annInputs);
    }
}
