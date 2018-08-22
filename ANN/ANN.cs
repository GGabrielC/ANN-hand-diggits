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
        Matrix<Double>[] x;
        Matrix<Double>[] y;

        int TestCasesCount { get => this.x.Count(); }
        int InputSize { get => this.x[0].RowCount; }
        int OutputSize { get => this.y[0].RowCount; }
        
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
            setNetworkInput(data);
            setExpectedNetworkOutput(data);
            setTopology();
            setWeights();
            setBiases();
            setActivationFunctions(ReLU);
        }

        void setNetworkInput(IEnumerable<MNIST.IO.TestCase> data)
        {
            this.x = new Matrix<Double>[data.Count()];
            int i = 0;
            foreach (var testCase in data)
                this.x[i++] = Matrix<Double>.Build.SparseOfArray(asOneCollumn(testCase.Image));
        }

        void setExpectedNetworkOutput(IEnumerable<MNIST.IO.TestCase> data)
        {
            this.y = new Matrix<Double>[data.Count()];
            int i = 0;
            foreach (var testCase in data)
            {
                var expectedOutput = new Double[10, 1];
                expectedOutput[testCase.Label, 0] = 1;
                this.y[i++] = Matrix<Double>.Build.SparseOfArray(expectedOutput);
            }
        }

        Double[,] asOneCollumn(byte[,] array)   
        {
            var output = new Double[array.Length, 1];
            int i = 0;
            foreach(var el in array)
                output[i++,0] = el;
            return output;
        }

        void setTopology()
        {
            this.layersCount = 5;
            this.layerSizes = new int[this.layersCount];
            this.layerSizes[0] = this.InputSize;
            this.layerSizes[this.layerSizes.Length - 1] = this.OutputSize;
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
            b = new Matrix<Double>[this.layersCount];
            for (var i = 1; i < this.layersCount-1; i++)
                b[i] = Matrix<Double>.Build.Random(this.layerSizes[i], 1);
        }

        void setActivationFunctions(Func<Double, Double> activationFunction)
        {

        }

        public void train()
        {

        }

    }
}
