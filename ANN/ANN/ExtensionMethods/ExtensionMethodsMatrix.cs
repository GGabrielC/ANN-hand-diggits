using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FuncDD = System.Func<System.Double, System.Double>;
using MatrixD = MathNet.Numerics.LinearAlgebra.Matrix<System.Double>;

namespace ExtensionMethods
{
    public static class ExtensionMethodsMatrix
    {
        public static MatrixD addEachLine(this MatrixD matrix, Vector<Double> numbers)
        {
            MatrixD m = MatrixD.Build.DenseOfMatrix(matrix);
            for (int i = 0; i < m.RowCount; i++)
                for (int j = 0; j < m.ColumnCount; j++)
                    m[i, j] = m[i, j] + numbers[j];
            return m;
        }

        public static MatrixD applyEachLine(this MatrixD matrix, List<FuncDD> func)
        {
            var m = MatrixD.Build.DenseOfMatrix(matrix);
            for (int i = 0; i < m.RowCount; i++)
                for (int j = 0; j < m.ColumnCount; j++)
                    m[i, j] = func[j](m[i, j]);
            return m;
        }

        public static MatrixD scalarMultiplication(this MatrixD m1, MatrixD m2)
        {
            var m = MatrixD.Build.DenseOfMatrix(m1);
            for (var i = 0; i < m.RowCount; i++)
                for (var j = 0; j < m.ColumnCount; j++)
                    m[i, j] *= m2[i, j];
            return m;
        }
    }
}
