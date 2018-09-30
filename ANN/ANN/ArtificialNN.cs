using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using Layers;
using CloneExtensions;
using MatrixD = MathNet.Numerics.LinearAlgebra.Matrix<System.Double>;
using SysColGen = System.Collections.Generic;
using ExtensionMethods;

namespace ANN
{
    public class ArtificalNN
    {
        public int OutputSize { get => this.outputSize; }
        public int InputSize { get => this.inputSize; }
        public int LayersCount { get => this.hiddenLayers.Count(); }
        
        public const int DEFAULT_LAYER_COUNT = 5;
        public const int DEFAULT_LAYER_SIZE = 5;

        ANNLayer InputLayer { get => this.hiddenLayers.First.Value; }
        ANNLayer OutputLayer { get => this.hiddenLayers.Last.Value; }
        public LinkedList<ANNLayer> LayersShallowCopy { get => this.hiddenLayers.ShallowCopy(); }

        LinkedList<ANNLayer> hiddenLayers = null;
        int inputSize;
        int outputSize;

        public ArtificalNN(int inputSize, int outputSize)
        {
            this.inputSize = inputSize;
            this.outputSize = outputSize;
            setLayers(DEFAULT_LAYER_COUNT, DEFAULT_LAYER_SIZE);
        }

        public MatrixD feed(MatrixD networkInput)
            => this.InputLayer.feedforward(networkInput);
        
        public void train(MatrixD annInput, MatrixD annExpectedOutput)
           => new Backpropagation().train(this, annInput, annExpectedOutput);
        
        public void setLayers(LinkedList<ANNLayer> layers)
            => this.hiddenLayers = layers;

        public void setLayers(int layersCount, int layerSize)
        {
            hiddenLayers = new LinkedList<ANNLayer>();
            hiddenLayers.AddFirst(new HiddenLayer(layerSize, this.inputSize));
            while (hiddenLayers.Count() < layersCount)
                hiddenLayers.AddLast(new HiddenLayer(layerSize, this.hiddenLayers.Last.Value.LayerOutputSize));
        }
    }
}
