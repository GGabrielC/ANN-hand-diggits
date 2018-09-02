using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using System.Linq;
using Layers;
using MatrixD = MathNet.Numerics.LinearAlgebra.Matrix<System.Double>;

namespace ANN
{
    public class ArtificalNN
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
        
        public ArtificalNN(int inputSize, int outputSize)
        {
            this.inputSize = inputSize;
            this.outputSize = outputSize;
            setLayers(DEFAULT_LAYER_COUNT, DEFAULT_LAYER_SIZE);
        }

        public MatrixD Feed(MatrixD networkInput)
        {
            var networkOutput = networkInput;
            foreach (var layer in layers)
                networkOutput = layer.feed(networkOutput);
            return networkOutput;
        }

        public void feedForTrain(MatrixD networkInput)
        {
            var networkOutput = networkInput;
            layers.First().feedForTrain(networkOutput);
        }

        public void backPropagation()
        {
            throw new NotImplementedException();
        }

        public void train(MatrixD input, MatrixD expectedOutput)
        {
            var costLayer = new CostLayer(expectedOutput);
            feedForTrain(input);

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
