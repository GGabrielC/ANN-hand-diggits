using ANN;
using ExtensionMethods;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MatrixD = MathNet.Numerics.LinearAlgebra.Matrix<System.Double>;

namespace ANN
{
    public class CostLayer : Layer
    {
        public int InSize => this.expectedInput.RowCount;
        public int OutSize => this.expectedInput.RowCount;

        public MatrixD lastCalculatedCost = null;
        private MatrixD expectedInput;

        public CostLayer(MatrixD expectedInput)
            => this.expectedInput = expectedInput;
        
        public MatrixD forward(MatrixD layerInput)
            => squaredEuclidianDistance(layerInput, expectedInput);
        
        public MatrixD backward(MatrixD inputs, MatrixD gradient=null)
            => getDerivateToInput(inputs);

        public void backwardLearn(MatrixD inputs, MatrixD gradient, double learnRate)
        { }
        
        public MatrixD getDerivateToInput(MatrixD inputs)
            => 2*(this.expectedInput - inputs);

        private static MatrixD squaredEuclidianDistance(MatrixD layerInput, MatrixD expectedInput)
        {
            var errorMatrix = expectedInput - layerInput;
            var squaredErrorMatrix = errorMatrix.PointwisePower(2);
            var cost = squaredErrorMatrix.RowSums().Divide(2);
            var result = MatrixD.Build.DenseOfColumnVectors(cost); // to check
            return result;
        }
    }
}
