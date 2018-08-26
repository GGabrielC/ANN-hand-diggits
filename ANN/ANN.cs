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
        public int OutputSize { get => this.outputSize; }
        public int InputSize { get => this.inputSize; }

        const int DEFAULT_LAYER_COUNT = 5;
        const int DEFAULT_LAYER_SIZE = 5;
        
        int inputSize;
        int outputSize;
        Layer[] layers;
        
        public ANN(int inputSize, int outputSize)
        {
            this.inputSize = inputSize;
            this.outputSize = outputSize;
            setLayers(DEFAULT_LAYER_COUNT, DEFAULT_LAYER_SIZE);
        }

        public Matrix<Double> feedForward(Matrix<Double> networkInput)
        {
            var networkOutput = networkInput;
            foreach (var layer in layers)
                networkOutput = layer.feed(networkOutput);
            return networkOutput;
        }

        public void train(Matrix<Double> input, Matrix<Double> expectedOutput)
        {
            var networkOutput = feedForward(input);
            var cost = getCost(input, networkOutput);
            // TODO
        }

        Vector<Double> getCost(Matrix<Double> networkOutput, Matrix<Double> expectedOutput)
        {
            var errorMatrix = expectedOutput - networkOutput;
            var squaredErrorMatrix = errorMatrix.PointwisePower(2);
            return squaredErrorMatrix.ColumnSums().Divide(2);
        }

        void setLayers(int layersCount, int layerSize)
        {
            layers = new Layer[layersCount];
            this.layers[0] = new Layer(this.inputSize, null);
            for (int i = 1; i < layers.Count()-1; i++)
                this.layers[i] = new Layer(layerSize, layers[i-1]);
            this.layers[layersCount - 1] = new Layer(this.outputSize, layers[layersCount-2]);
        }


    }
}
