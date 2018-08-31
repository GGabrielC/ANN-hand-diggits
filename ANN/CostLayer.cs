using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace ANN
{
    public class CostLayer : ANNLayer
    {
        public override int Size { get => this.expectedOutput.RowCount; }

        Matrix<Double> expectedOutput;

        public CostLayer(Matrix<Double> expectedOutput) => this.expectedOutput = expectedOutput;

        public override Matrix<double> feed(Matrix<double> networkOutput) =>
            CostFunctions.squaredEuclidianDistance(networkOutput, expectedOutput);

        public override Matrix<double> feedForTrain(Matrix<double> input)
        {
            throw new NotImplementedException();
        }
    }
}
