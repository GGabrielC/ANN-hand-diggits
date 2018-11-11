﻿using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using FuncDD = System.Func<System.Double, System.Double>;
using MatrixD = MathNet.Numerics.LinearAlgebra.Matrix<System.Double>;

namespace ExtensionMethods
{
    public static class ExtensionMethodsMatrixD
    {
        public static MatrixD repeat(this MatrixBuilder<double> mb, int rows, int cols, double value)
        {
            var matrix = new double[rows, cols];
            for (var i = 0; i < rows; i++)
                for (var j = 0; j < cols; j++)
                    matrix[i, j] = value;
            return MatrixD.Build.DenseOfArray(matrix);
        }

        public static MatrixD addEachLine(this MatrixD matrix, double[] numbers)
        {
            MatrixD m = MatrixD.Build.DenseOfMatrix(matrix);
            for (int i = 0; i < m.RowCount; i++)
                for (int j = 0; j < m.ColumnCount; j++)
                    m[i, j] = m[i, j] + numbers[j];
            return m;
        }

        public static MatrixD mapLines(this MatrixD matrix, FuncDD[] func)
        {
            var m = MatrixD.Build.DenseOfMatrix(matrix);
            for (int i = 0; i < m.RowCount; i++)
                for (int j = 0; j < m.ColumnCount; j++)
                    m[i, j] = func[j](m[i, j]);
            return m;
        }

        public static MatrixD map(this MatrixD matrix, FuncDD func)
        {
            var m = MatrixD.Build.DenseOfMatrix(matrix);
            for (int i = 0; i < m.RowCount; i++)
                for (int j = 0; j < m.ColumnCount; j++)
                    m[i, j] = func(m[i, j]);
            return m;
        }

        public static MatrixD scalarMultiply(this MatrixD m1, MatrixD m2)
        {
            var m = MatrixD.Build.DenseOfMatrix(m1);
            for (var i = 0; i < m.RowCount; i++)
                for (var j = 0; j < m.ColumnCount; j++)
                    m[i, j] *= m2[i, j];
            return m;
        }

        public static MatrixD scalarMultiply(this MatrixD m1, double scalar)
        {
            var m = MatrixD.Build.DenseOfMatrix(m1);
            for (var i = 0; i < m.RowCount; i++)
                for (var j = 0; j < m.ColumnCount; j++)
                    m[i, j] *= scalar;
            return m;
        }

        public static bool EEquals(this MatrixD m1, MatrixD m2, double epsilon=0.000001)
        {
            if (m1.RowCount != m2.RowCount || m1.ColumnCount != m2.ColumnCount)
                return false;
            for (var i = 0; i < m1.RowCount; i++)
                for (var j = 0; j < m1.ColumnCount; j++)
                    if (! m1[i, j].EEquals(m2[i, j], epsilon))
                        return false;
            return true;
        }

        public static void print(this MatrixD m)
        {
            Console.Write("[");
            for (var i = 0; i < m.RowCount; i++)
            {
                Console.Write("[");
                for (var j = 0; j < m.ColumnCount; j++)
                    Console.Write(m[i,j]+", ");
                Console.WriteLine("]");
            }
            Console.WriteLine("]");
        }

        public static MultiMatrix[] toMultiMatrix(this MatrixD data, int[] entryDimensions)
        {
            int countEntries = data.RowCount;
            var dataArr = data.AsRowArrays();
            MultiMatrix[] matrices = new MultiMatrix[countEntries];
            for (int i = 0; i < countEntries; i++)
                matrices[i] = new MultiMatrix(entryDimensions, dataArr[i]);
            return matrices;
        }
    }
}
