using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatrixD = MathNet.Numerics.LinearAlgebra.Matrix<System.Double>;

namespace ANN
{
    public interface Layer
    {
        int InSize { get; }
        int OutSize { get; }
        MatrixD forward(MatrixD inputs);
        MatrixD backward(MatrixD inputs, MatrixD gradient);
        void backwardLearn(MatrixD inputs, MatrixD gradient, double learnRate);
    }
}
