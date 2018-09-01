using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethods
{
    public static class ExtensionMethodsMatrix
    {
        public static Matrix<Double> addEachLine(this Matrix<Double> matrix, Vector<Double> numbers)
        {
            Matrix<Double> m = Matrix<Double>.Build.DenseOfMatrix(matrix);
            for (int i = 0; i < m.RowCount; i++)
                for (int j = 0; j < m.ColumnCount; j++)
                    m[i, j] = m[i, j] + numbers[j];
            return m;
        }

        public static Matrix<Double> applyEachLine(this Matrix<Double> matrix, Func<Double, Double>[] func)
        {
            var m = Matrix<Double>.Build.DenseOfMatrix(matrix);
            for (int i = 0; i < m.RowCount; i++)
                for (int j = 0; j < m.ColumnCount; j++)
                    m[i, j] = func[j](m[i, j]);
            return m;
        }
    }
}
