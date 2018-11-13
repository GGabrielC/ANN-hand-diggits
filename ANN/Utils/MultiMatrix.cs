using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtensionMethods;
using Global;
using FuncDD = System.Func<System.Double, System.Double>;
using MatrixD = MathNet.Numerics.LinearAlgebra.Matrix<System.Double>;

namespace Utils
{
    public class MultiMatrix
    {
        public static readonly MultiMatrixBuilder Build = new MultiMatrixBuilder();

        public double[] Data { get => this.data; }
        public int[] Dimensions {
            get => this.dimensions;
            set => this.dimensions = value.Where(x => x > 1).ToArray();
        }
        public int DimensionCount { get => this.dimensions.Length; }
        public int Capacity { get => this.Data.Length; }

        double[] data;
        int[] dimensions;

        public MultiMatrix()
        { }

        public MultiMatrix(int[] dimensions)
        {
            Dimensions = dimensions;
            setMatrixSpace();
        }

        public MultiMatrix(MultiMatrix[] multiMatrices)
        {
            setDimensions(multiMatrices);
            setMatrixSpace(multiMatrices);
        }

        public MultiMatrix(MultiMatrix multiMatrix)
            => setData(multiMatrix.data, multiMatrix.dimensions);
        
        public MultiMatrix(int[] dimensions, double[] data)
            => setData(data, dimensions);

        public bool EEquals(MultiMatrix mm)
            => this.dimensions.EEquals(mm.dimensions) && this.data.EEquals(mm.data);

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
            var m = new MultiMatrix(this.dimensions.add(padding).add(padding));
            foreach (var coords in this.AllCoords())
                m.setAt(coords.add(padding), this.at(coords));
            return m;
        }

        public int[] getNextCoords(int[] coordinates, int stride=1)
        {
            var next = coordinates.ShallowCopy();
            for (int i = next.Length - 1; i >= 0; i--)
            {
                next[i] += stride;
                if (isValidCoordinate(next[i], i))
                    return next;
                next[i] = 0;
            }
            return null;
        }

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

        public MultiMatrix scalarMultiply(MultiMatrix m)
        {
            var result = new MultiMatrix();
            result.useData(this.data.scalarMultiply(m.data), this.dimensions);
            return result;
        }

        public MultiMatrix scalarMultiply(double scalar)
        {
            var result = new MultiMatrix();
            result.useData(this.data.scalarMultiply(scalar), this.dimensions);
            return result;
        }

        public MultiMatrix add(MultiMatrix m)
        {
            if (!this.Dimensions.EEquals(m.Dimensions))
                throw new Exception("Matrices have diffrent dimensions.They cannot be added.");
            return new MultiMatrix(this.dimensions, this.Data.add(m.Data));
        }

        public MultiMatrix map(FuncDD func)
        {
            var newMatrix = new MultiMatrix();
            newMatrix.useData(this.data.map(func), this.dimensions);
            return newMatrix;
        }

        public void setRandomData()
        {
            for (int i = 0; i < this.data.Length; i++)
                this.data[i] = GlobalRandom.NextDouble();
        }

        public void setRandomData(double minVal, double maxVal)
        {
            for (int i = 0; i < this.data.Length; i++)
                this.data[i] = GlobalRandom.NextDouble(minVal, maxVal);
        }

        public void setData(double[] data, int[] dimensions)
        {
            if (data.Length != dimensions.product())
                throw new Exception("data does not have provided element count");
            this.data = data.ShallowCopy();
            this.Dimensions = dimensions.ShallowCopy();
        }

        public void useData(double[] data, int[] dimensions)
        {
            this.data = data;
            this.Dimensions = dimensions;
        }
        
        private void setDimensions(MultiMatrix[] multiMatrices)
        {
            this.dimensions = new int[1 + multiMatrices[0].DimensionCount];
            this.dimensions[0] = multiMatrices.Length;
            multiMatrices[0].dimensions.CopyTo(this.dimensions, 1);
        }
        
        private void setMatrixSpace()
            => this.data = new double[this.dimensions.product()];

        private void setMatrixSpace(MultiMatrix[] multiMatrices)
        {
            setMatrixSpace();
            var dim0Capacity = multiMatrices[0].Capacity;
            for (int i = 0; i < multiMatrices.Length; i++)
                multiMatrices[i].Data.CopyTo(this.Data, i*dim0Capacity);
        }
    }
}
