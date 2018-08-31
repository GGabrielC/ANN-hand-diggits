using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANN
{
    public class CostFunctions
    {
        public static Matrix<double> squaredEuclidianDistance(Matrix<Double> networkOutput, Matrix<Double> expectedOutput)
        {
            var errorMatrix = expectedOutput - networkOutput;
            var squaredErrorMatrix = errorMatrix.PointwisePower(2);
            var cost = squaredErrorMatrix.ColumnSums().Divide(2);
            return Matrix<double>.Build.SameAs(cost, 1, cost.Count); // TODO check SameAs
        }
    }
}
