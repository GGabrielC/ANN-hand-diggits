using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using Layers;
using MatrixD = MathNet.Numerics.LinearAlgebra.Matrix<System.Double>;
using MathNet.Numerics.LinearAlgebra;

namespace ANN
{
    public class Network: LayeredANN
    {
        public int LayersCount { get => layers.Count(); }
        public IEnumerable<Layer> Layers { get => layers; }
        
        public int InSize => inSize;
        public int OutSize => outSize;

        List<Layer> layers = new List<Layer>();
        int LastLayerOutSize => layers.Last().OutSize;

        int inSize;
        int outSize;
        
        public Network(int inSize, int outSize)
        {
            this.inSize = inSize;
            this.outSize = outSize;
            addLayers();
        }

        public MatrixD feedForward(MatrixD inputs)
        {
            var leyerOutput = inputs;
            foreach(var layer in layers)
                leyerOutput = layer.forward(leyerOutput);
            return leyerOutput;
        }

        public void train(MatrixD annInput, MatrixD annExpectedOutput)
            => new Backprop(this, annInput, annExpectedOutput).train();

        public void addLayer(Layer layer)
            => layers.Add(layer);

        public void setLayers(Layer[] layers)
            => this.layers = new List<Layer>(layers); //TO CHECK ORDER

        private void addLayers()
        {
            /*
            var depth = 3;
            addLayer(new ConvolutionLayer(new int[] { 3, 3 }, depth, inSize));
            addLayer(new ConvolutionLayer(new int[] { depth, 3, 3 }, depth, LastLayerOutSize));
            addLayer(new NormalizationLayer(LastLayerOutSize));
            addLayer(new PoolingLayer(new int[] { depth, 3, 3 }, LastLayerOutSize));

            addLayer(new ConvolutionLayer(new int[] { 3, 3 }, depth, LastLayerOutSize));
            addLayer(new ConvolutionLayer(new int[] { depth, 3, 3 }, depth, LastLayerOutSize));
            addLayer(new NormalizationLayer(LastLayerOutSize));
            addLayer(new PoolingLayer(new int[] { depth, 3, 3 }, LastLayerOutSize));
            */
            int countNeuronLeyers = 5;
            for (var i = 0; i < countNeuronLeyers; i++)
            {
                var layerOutSize = (i + 1 == countNeuronLeyers ? 10 : 5);
                addLayer(new WeightLayer(LastLayerOutSize, layerOutSize));
                addLayer(new BiasLayer(LastLayerOutSize));
                addLayer(new ActivationLayer(LastLayerOutSize));
            }
        }
    }
}
