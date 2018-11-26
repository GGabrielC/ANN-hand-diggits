using MultiMatrix_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatrixD = MathNet.Numerics.LinearAlgebra.Matrix<System.Double>;

namespace LayerInputExtensions
{
    public static class ExtensionsLayerInputConverter
    {
        public static MultiMatrix[] toMultiMatrix(this MatrixD data, int[] entryDimensions)
        {
            int countEntries = data.RowCount;
            var dataArr = data.ToRowArrays();
            MultiMatrix[] matrices = new MultiMatrix[countEntries];
            for (int i = 0; i < countEntries; i++)
                matrices[i] = MultiMatrix.Build.useData(dataArr[i], entryDimensions);
            return matrices;
        }

        public static MatrixD toMatrixD(this MultiMatrix[] data)
        {
            int countEntries = data.Length;
            var dataArr = new double[countEntries][];
            for (var i = 0; i < countEntries; i++)
                dataArr[i] = data[i].Data;
            return MatrixD.Build.DenseOfRowArrays(dataArr);
        }
    }
}
