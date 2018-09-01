using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class CostFunctions
    {
        public static Matrix<double> squaredEuclidianDistance(Matrix<Double> networkOutput, Matrix<Double> expectedOutput)
        {

            var errorMatrix = expectedOutput - networkOutput;
            var squaredErrorMatrix = errorMatrix.PointwisePower(2);
            var cost = squaredErrorMatrix.ColumnSums().Divide(2);
            var result = Matrix<double>.Build.DenseOfColumnVectors(cost);
            return result;
        }
    }
}
