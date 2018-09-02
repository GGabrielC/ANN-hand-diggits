using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using Utils;
using MatrixD = MathNet.Numerics.LinearAlgebra.Matrix<System.Double>;

namespace Layers
{
    public class CostLayer : ANNLayer
    {
        public override int Size { get => this.expectedOutput.RowCount; }

        private MatrixD expectedOutput;
        private MatrixD networkOutput;
        private MatrixD cost;
        private Layer previousLayer;

        public CostLayer(MatrixD expectedOutput) 
            => this.expectedOutput = expectedOutput;

        public override MatrixD feed(MatrixD networkOutput) 
            => squaredEuclidianDistance(networkOutput, expectedOutput);

        public override void feedForTrain(MatrixD networkOutput)
            => this.cost = squaredEuclidianDistance(networkOutput, expectedOutput);
        
        public override void backPropagate(MatrixD notNeeded=null) // TODO
        {
            var derivate = - (this.expectedOutput - this.networkOutput);
            previousLayer.backPropagate(derivate);
        }
        
        private static MatrixD squaredEuclidianDistance(MatrixD networkOutput, MatrixD expectedOutput)
        {
            var errorMatrix = expectedOutput - networkOutput;
            var squaredErrorMatrix = errorMatrix.PointwisePower(2);
            var cost = squaredErrorMatrix.ColumnSums().Divide(2);
            var result = MatrixD.Build.DenseOfColumnVectors(cost);
            return result;
        }
    }
}
