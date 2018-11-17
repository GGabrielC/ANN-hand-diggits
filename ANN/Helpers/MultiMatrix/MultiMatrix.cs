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
            get => this.dimensions.ShallowCopy();
            set
            {
                if (value.Length == 0)
                    throw new Exception("Cannot Create 0-dim matrix!");
                if (!value.All(x => x > 0))
                    throw new Exception("You cannot have less than 1 element in a dimension!");
                if (Capacity != value.product())
                    throw new Exception("Dimensions can not fit the data size!");
                this.dimensions = value;
                setCapacityCumulation();
            }
        }
        public int DimensionCount { get => this.dimensions.Length; }
        public int Capacity { get => this.Data.Length; }

        double[] data;
        int[] dimensions;
        int[] capacityCumulation;

        public MultiMatrix()
        { }

        public MultiMatrix(MultiMatrix multiMatrix)
            => set(multiMatrix.data, multiMatrix.dimensions);

        public MultiMatrix(int[] dimensions)
            => set(new double[dimensions.product()], dimensions);

        public MultiMatrix(int[] dimensions, double[] data)
            => set(data, dimensions);

        public MultiMatrix(int[] dimensions, IEnumerable<double> data)
            => set(data.ToArray(), dimensions);

        public MultiMatrix(MultiMatrix[] multiMatrices)
            => use(getMergedData(multiMatrices), getMergedDimensions(multiMatrices));

        public MultiMatrix copy()
            => new MultiMatrix(this);
        
        public bool EEquals(MultiMatrix mm)
            => this.dimensions.EEquals(mm.dimensions) && this.data.EEquals(mm.data);
        
        public void set(double[] data, int[] dimensions)
        {
            this.data = data.ShallowCopy();
            this.Dimensions = dimensions.ShallowCopy();
        }

        public void use(double[] data, int[] dimensions)
        {
            this.data = data;
            this.Dimensions = dimensions;
        }

        public void setRandomData()
            => GlobalRandom.SetRandom(this.data);

        public void setRandomData(double minVal, double maxVal)
            => GlobalRandom.SetRandom(this.data, minVal, maxVal);

        public void cleanDimensions()
            => this.Dimensions = this.dimensions.Where(d=>d>1).ToArray();

        public void unclearDimensions(int[] dims)
        {
            if (!dims.Where(d => d > 1).ToArray().EEquals(dims))
                throw new Exception("Dimension is not compatible!");
            this.Dimensions = dims;
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
                index += coordinates[i] * this.capacityCumulation[i+1];
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

        public MultiMatrix scalarMultiply(MultiMatrix m)
            => MultiMatrix.Build.useData(this.data.scalarMultiply(m.data), this.dimensions);

        public MultiMatrix scalarMultiply(double scalar)
            => MultiMatrix.Build.useData(this.data.scalarMultiply(scalar), this.dimensions);

        public static MultiMatrix operator +(MultiMatrix m1, MultiMatrix m2)
            => MultiMatrix.Build.useData(m1.Data.add(m2.Data), m1.dimensions);

        public static void addIn(MultiMatrix m1, MultiMatrix m2)
            => m1.Data.addIn(m2.Data);

        public MultiMatrix map(FuncDD func)
            => MultiMatrix.Build.useData(this.data.map(func), this.dimensions);

        public override string ToString()
        {
            var str = "";
            for (var i = 0; i < dimensions.Length - 1; i++)
                str += dimensions[i] + "x";
            str += dimensions.Last() + " matrix";
            return str;
        }

        private void setCapacityCumulation()
        {
            capacityCumulation = new int[dimensions.Length];
            capacityCumulation[dimensions.Length-1] = dimensions.Last();
            for (int i= dimensions.Length-2; i>=0; i--)
                capacityCumulation[i] = dimensions[i] * capacityCumulation[i + 1];
        }

        public MultiMatrix[] split()
        {
            var dims = this.dimensions.Skip(1).ToArray();
            var capacity = dims.product();
            var mms = new MultiMatrix[this.dimensions[0]];
            for (int i = 0; i < mms.Length; i++)
                mms[i] = new MultiMatrix(dims, this.data.Skip(i * capacity).Take(capacity));
            return mms;
        }

        private int[] getMergedDimensions(MultiMatrix[] multiMatrices)
        {
            if (!multiMatrices.All(mm => multiMatrices[0].dimensions.EEquals(mm.dimensions)))
                throw new Exception("All matrices should be of same dimensions!");
            var dimensions = new int[1 + multiMatrices[0].dimensions.Length];
            dimensions[0] = multiMatrices.Length;
            multiMatrices[0].dimensions.CopyTo(dimensions, 1);
            return dimensions;
        }

        private double[] getMergedData(MultiMatrix[] multiMatrices)
        {
            if (!multiMatrices.All(mm => multiMatrices[0].dimensions.EEquals(mm.dimensions)))
                throw new Exception("All matrices should be of same dimensions!");
            var data = new double[multiMatrices.Count() * multiMatrices[0].Capacity];
            for (int i = 0; i < multiMatrices.Length; i++)
                multiMatrices[i].Data.CopyTo(data, i * multiMatrices[0].Capacity);
            return data;
        }
    }
}
