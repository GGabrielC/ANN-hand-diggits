using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ANN;
using MathNet.Numerics.LinearAlgebra;
using Utils;
using MatrixD = MathNet.Numerics.LinearAlgebra.Matrix<System.Double>;

namespace Layers
{
    public class CostLayer : ANNLayer
    {
        public override int LayerInputSize { get => this.expectedInput.RowCount; }
        public override int LayerOutputSize { get => 1; }
        public MatrixD lastCalculatedCost = null;
        private MatrixD expectedInput;

        public CostLayer(MatrixD expectedInput, ANNLayer nextLayer = null)
        {
            this.expectedInput = expectedInput;
            base.NextLayer = null;
        }

        public override MatrixD feed(MatrixD layerInput)
            => squaredEuclidianDistance(layerInput, expectedInput);

        public MatrixD calculateDerivate(MatrixD layerInput)
            => this.expectedInput - layerInput;

        public override void accept(ANNTrainManager visitor)
            => visitor.visit(this);

        private static MatrixD squaredEuclidianDistance(MatrixD layerInput, MatrixD expectedInput)
        {
            var errorMatrix = expectedInput - layerInput;
            var squaredErrorMatrix = errorMatrix.PointwisePower(2);
            var cost = squaredErrorMatrix.ColumnSums().Divide(2);
            var result = MatrixD.Build.DenseOfColumnVectors(cost);
            return result;
        }
    }
}
