using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalItems;

namespace Layers
{
    public class Filter3D
    {
        public int Dim1 { get => this.matrix.Length; }
        public int Dim2 { get => this.matrix[0].Length; }
        public int Dim3 { get => this.matrix[0][0].Length; }
        double[][][] matrix;

        public Filter3D(int dim1, int dim2, int dim3)
            => initFilterMatrix(dim1, dim2, dim3);
        
        public double[][][] filterImage(double[][][] image)
        {
            double[][][] filtered = getFilterOutputSizedMatrix(image);
            for (int i = 0; i < filtered.Length; i++)
            for (int j = 0; j < filtered.Length; j++)
            for (int k = 0; k < filtered.Length; k++)
            {
                filtered[i][j][k] = 0;
                for (int x = 0; x < Dim1; x++)
                for (int y = 0; y < Dim2; y++)
                for (int z = 0; z < Dim3; z++)
                    filtered[i][j][k] += matrix[x][y][z]*image[i+x][j+y][k+z];
            }
            return filtered;
        }

        private double[][][] getFilterOutputSizedMatrix(double[][][] image)
        {
            var m = new double[image.Length-Dim1/2][][];
            for(var i=0; i<image.Length; i++)
            {
                m[i] = new double[image[0].Length - Dim2 / 2][];
                for (var j = 0; j < image[0].Length; j++)
                    m[i][j] = new double[image[0][0].Length - Dim3/2] ;
            }
            return m;
        }

        private void initFilterMatrix(int dim1, int dim2, int dim3)
        {
            this.matrix = new double[dim1][][];
            for (int i = 0; i < dim1; i++)
            {
                this.matrix[i] = new double[dim2][];
                for (int j = 0; j < dim2; j++)
                {
                    this.matrix[i][j] = new double[dim3];
                    for (int k = 0; k < dim3; k++)
                        matrix[i][j][k] = GlobalRandom.Instance.NextDouble(); //TODO range
                }
            }
        }
    }
}
