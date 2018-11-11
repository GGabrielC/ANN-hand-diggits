using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtensionMethods;
using GlobalItems;
using FuncDD = System.Func<System.Double, System.Double>;
using MatrixD = MathNet.Numerics.LinearAlgebra.Matrix<System.Double>;

namespace Utils
{
    public class MultiMatrix
    {
        public double[] Data { get => this.data; }
        public int[] Dimensions { get => this.dimensions; }
        public int DimensionCount { get => this.dimensions.Length; }
        public int Capacity { get => this.Data.Length; }

        double[] data;
        int[] dimensions;

        public MultiMatrix(int[] dimensions)
        {
            setDimensions(dimensions);
            setMatrixSpace();
        }

        public MultiMatrix(MultiMatrix[] multiMatrices)
        {
            setDimensions(multiMatrices);
            setMatrixSpace(multiMatrices);
        }

        public MultiMatrix(MultiMatrix multiMatrix)
        {
            setDimensions(multiMatrix.dimensions);
            setData(multiMatrix.Data);
        }

        public MultiMatrix(int[] dimensions, double[] data)
        {
            this.dimensions = dimensions.ShallowCopy();
            this.data = data;
        }
        
        public double at(int[] coordinates)
            => data[findIndex(coordinates)];

        public void setAt(int[] coordinates, double value=0)
            => data[findIndex(coordinates)] = value;

        public void addAt(int[] coordinates, double value = 0)
            => data[findIndex(coordinates)] += value;

        public int findIndex(int[] coordinates)
        {
            int index = 0;
            for (int i = 0; i < coordinates.Length - 1; i++)
                index += coordinates[i] * this.dimensions[i];
            index += coordinates[coordinates.Length - 1];
            return index;
        }
        
        public int[] firstCoords()
            => new int[dimensions.Length];
        
        public bool areValidCoords(int[] coordinates)
            => coordinates!= null && coordinates.AllIndex( isValidCoordinate );

        public bool isValidCoordinate(int coordinate, int axis)
            => coordinate >= 0 && coordinate < this.dimensions[axis];
        
        /*
        public bool areLastCoords(int[] coordinates)
            => coordinates.AllIndex(isLastCoord);

        public bool isLastCoord(int coordinate, int axis)
            => coordinate == Dimensions[axis] - 1;
        //*/

        public System.Collections.Generic.IEnumerable<int[]> AllCoords()
        {
            foreach (var c in AllCoords(ArrayBuilder.repeat(1, DimensionCount)))
                yield return c.ShallowCopy();
        }

        public System.Collections.Generic.IEnumerable<int[]> AllCoords(int[] strides)
        {
            for (var c = firstCoords(); c != null; c = getNextCoords(c, strides))
                yield return c.ShallowCopy();
        }

        public MultiMatrix padded(int[] padding)
        {
            var m = new MultiMatrix(this.dimensions.addTo(padding).addTo(padding));
            foreach (var coords in this.AllCoords())
                m.setAt(coords.addTo(padding), this.at(coords));
            return m;
        }

        public int[] getNextCoords(int[] coordinates)
            => getNextCoords(coordinates, ArrayBuilder.repeat(1, coordinates.Length));

        public int[] getNextCoords(int[] coordinates, int[] strides)
        {
            var next = coordinates.ShallowCopy();
            for (int i = next.Length - 1; i >= 0; i--)
            {
                next[i] += strides[i];
                if (isValidCoordinate(next[i], i))
                    return next;
                next[i] = 0;
            }
            return null;
        }

        public MultiMatrix copy()
            => new MultiMatrix(this);

        public MultiMatrix map(FuncDD func)
        {
            var newMatrix = new MultiMatrix(this.dimensions);
            newMatrix.data = this.data.map(func);
            return newMatrix;
        }

        public void setRandom()
        {
            for (int i = 0; i < this.data.Length; i++)
                this.data[i] = GlobalRandom.Instance.NextDouble(); //TODO range
        }
        
        private void setDimensions(int[] dimensions)
            => this.dimensions = dimensions.Where(x => x > 1).ToArray();

        private void setDimensions(MultiMatrix[] multiMatrices)
        {
            this.dimensions = new int[1 + multiMatrices[0].DimensionCount];
            this.dimensions[0] = multiMatrices.Length;
            multiMatrices[0].dimensions.CopyTo(this.dimensions, 1);
        }

        private void setData(double[] data)
            => this.data = data.ShallowCopy();

        private void setMatrixSpace()
            => this.data = new double[this.dimensions.multiplyTo()];

        private void setMatrixSpace(MultiMatrix[] multiMatrices)
        {
            setMatrixSpace();
            var dim0Capacity = multiMatrices[0].Capacity;
            for (int i = 0; i < multiMatrices.Length; i++)
                multiMatrices[i].Data.CopyTo(this.Data, i*dim0Capacity);
        }
    }
}
