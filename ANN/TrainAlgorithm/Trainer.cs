using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Layers;
using MatrixD = MathNet.Numerics.LinearAlgebra.Matrix<System.Double>;

namespace TrainAlgorithm
{
    public interface Trainer
    {
        void train(MatrixD annInputs, MatrixD annExpectedOutputs, int iterations = 100, int iterationSize = 50);
    }
}
