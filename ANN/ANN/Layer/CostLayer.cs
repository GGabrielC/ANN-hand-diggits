using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using Utils;

namespace Layers
{
    public class CostLayer : ANNLayer
    {
        public override int Size { get => this.expectedOutput.RowCount; }

        Matrix<Double> expectedOutput;
        Matrix<Double> cost;

        public CostLayer(Matrix<Double> expectedOutput) => this.expectedOutput = expectedOutput;

        public override Matrix<double> feed(Matrix<double> networkOutput) =>
            CostFunctions.squaredEuclidianDistance(networkOutput, expectedOutput);

        public override void feedForTrain(Matrix<double> networkOutput)
        {
            this.cost = CostFunctions.squaredEuclidianDistance(networkOutput, expectedOutput);
            //TODO
        }
    }
}
