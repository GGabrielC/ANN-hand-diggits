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
        const int DEFAULT_LAYER_COUNT = 5;
        const int DEFAULT_LAYER_SIZE = 5;

        Matrix<Double> x;
        Matrix<Double> y;

        int TestCasesCount { get => this.x.RowCount; }
        int TestCaseInputSize { get => this.x.ColumnCount; }
        int LabelsCount { get => this.y.ColumnCount; }

        Layer[] layers;
        
        public ANN(IEnumerable<MNIST.IO.TestCase> data)
        {
            setNetworkInput(data, 28*28);
            setExpectedNetworkOutput(data, 10);
            setLayers(DEFAULT_LAYER_COUNT, DEFAULT_LAYER_SIZE);
        }

        void setNetworkInput(IEnumerable<MNIST.IO.TestCase> data, int testCaseSize)
        {
            var rawMatrix = new Double[data.Count(), testCaseSize];
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
            var rawMatrix = new Double[this.TestCasesCount, labelsCount];
            int i = 0;
            foreach (var testCase in data)
                rawMatrix[i++, testCase.Label] = 1;
            this.y = Matrix<Double>.Build.SparseOfArray(rawMatrix);
        }

        void setLayers(int layersCount, int layerSize)
        {
            layers = new Layer[layersCount];
            this.layers[0] = new Layer(this.TestCaseInputSize, null);
            for (int i = 1; i < layers.Count()-1; i++)
                this.layers[i] = new Layer(layerSize, layers[i-1]);
            this.layers[layersCount - 1] = new Layer(this.LabelsCount, layers[layersCount-2]);
        }
        
        public Matrix<Double> feedForward(Matrix<Double> networkInput)
        {
            var networkOutput = networkInput;
            foreach (var layer in layers)
                networkOutput = layer.feed(networkOutput);
            return networkOutput;
        }

        public void train(Matrix<Double> networkInput)
        {
            var networkOutput = feedForward(networkInput);
        }
    }
}
