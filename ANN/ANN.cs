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
        public int LayersCount { get => this.layers.Count(); }
        public int getLayerSize(int layerIndex) => this.layers[layerIndex].Size;

        const int DEFAULT_LAYER_COUNT = 5;
        const int DEFAULT_LAYER_SIZE = 5;
        
        int inputSize;
        int outputSize;
        ANNLayer[] layers;
        
        public ANN(int inputSize, int outputSize)
        {
            this.inputSize = inputSize;
            this.outputSize = outputSize;
            setLayers(DEFAULT_LAYER_COUNT, DEFAULT_LAYER_SIZE);
        }

        public Matrix<Double> feed(Matrix<Double> networkInput)
        {
            var networkOutput = networkInput;
            foreach (var layer in layers)
                networkOutput = layer.feed(networkOutput);
            return networkOutput;
        }

        public Matrix<Double> feedTrain(Matrix<Double> networkInput)
        {
            var networkOutput = networkInput;
            foreach (var layer in layers)
                networkOutput = layer.feed(networkOutput);
            return networkOutput;
        }

        public void backPropagation()
        {
            throw new NotImplementedException();
        }

        public void train(Matrix<Double> input, Matrix<Double> expectedOutput)
        {
            var costLayer = new CostLayer(expectedOutput);
            var networkOutput = feedTrain(input);
            backPropagation();
            // TODO
        }
        
        void setLayers(int layersCount, int layerSize)
        {
            layers = new ANNLayer[layersCount];
            int i = 0;
            this.layers[i++] = new InputLayer(this.inputSize);
            while( i < layers.Count()-1 )
                this.layers[i++] = new Layer(layerSize, layers[i-1].Size);
            this.layers[i++] = new Layer(this.outputSize, layers[i-1].Size);
        }
    }
}
