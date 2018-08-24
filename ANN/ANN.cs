using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using System.Linq;

namespace ANN
{
    public class ANN
    {
        Matrix<Double> x;
        Matrix<Double> y;

        int TestCasesCount { get => this.x.RowCount; }
        int TestCaseInputSize { get => this.x.ColumnCount; }
        int LabelsCount { get => this.y.ColumnCount; }

        int layersCount;
        int[] layerSizes;

        Matrix<Double>[] w;
        Matrix<Double>[] b;
        Func<Double, Double>[] a;
        
        public Double ReLU(Double x) //TODO move
        {
            return x < 0 ? 0 : x;
        }

        public ANN(IEnumerable<MNIST.IO.TestCase> data)
        {
            setNetworkInput(data, 28*28);
            setExpectedNetworkOutput(data, 10);
            setTopology();
            setWeights();
            setBiases();
            setActivationFunctions(ReLU);
        }

        void setNetworkInput(IEnumerable<MNIST.IO.TestCase> data, int testCaseSize)
        {
            var testCasesCount = data.Count();
            var rawMatrix = new Double[testCasesCount, testCaseSize];
            int i = 0;
            foreach (var testCase in data) {
                int j = 0;
                foreach(var pixel in testCase.Image)
                    rawMatrix[i,j++] = pixel;
                i++;
            }
            this.x = Matrix<Double>.Build.SparseOfArray(rawMatrix);
        }

        void setExpectedNetworkOutput(IEnumerable<MNIST.IO.TestCase> data, int labelsCount)
        {
            int i = 0;
            var rawMatrix = new Double[this.TestCasesCount, labelsCount];
            foreach (var testCase in data)
                rawMatrix[i++, testCase.Label] = 1;
            this.y = Matrix<Double>.Build.SparseOfArray(rawMatrix);
        }

        void setTopology()
        {
            this.layersCount = 5;
            this.layerSizes = new int[this.layersCount];
            this.layerSizes[0] = this.TestCaseInputSize;
            this.layerSizes[this.layerSizes.Length - 1] = this.LabelsCount;
            for (var i = 1; i < this.layersCount - 1; i++)
                this.layerSizes[i] = 5;
        }
        
        void setWeights()
        {
            w = new Matrix<Double>[this.layersCount];
            for (var i = 1; i < this.layersCount; i++)
                w[i] = Matrix<Double>.Build.Random(this.layerSizes[i-1], this.layerSizes[i]);
        }

        void setBiases()
        {
            // TODO
            b = new Matrix<Double>[this.layersCount];
            for (var i = 1; i < this.layersCount-1; i++)
                b[i] = Matrix<Double>.Build.Random(this.layerSizes[i], 1);
        }
        
        void setActivationFunctions(Func<Double, Double> activationFunction)
        {
            // TODO
        }

        public void train()
        {
            var a = x * w[1];
        }

    }
}
