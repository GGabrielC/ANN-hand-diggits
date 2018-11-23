using ANN;
using Layers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using FuncDD = System.Func<System.Double, System.Double>;

namespace MNIST_SOLVER
{
    public class NetworkBuilder
    {
        const int IMAGE_HEIGHT = 28;
        const int IMAGE_WIDTH = 28;
        const int COUNT_LABELS = 10;

        LinkedList<Layer> layers = new LinkedList<Layer>();

        private int[] LastLayerOutDims => ((ConvNetLayer)(layers.Last())).OutDims;
        private int LastLayerOutSize => layers.Last().OutSize;

        public NetworkBuilder()
        {}

        public Network build()
        {
            var inDims = new int[] {IMAGE_HEIGHT, IMAGE_WIDTH };
            var inSize = COUNT_LABELS;
            var depth = 3;

            layers.AddLast(new ConvolutionLayer(new int[] { 3, 3 }, depth, inDims));
            addConvolutionLayer(new int[] { depth, 3, 3 }, depth);
            addNormalizationLayer();
            addPoolingLayer(new int[] { depth, 1, 2, 2 });

            int countNeuronLeyers = 5;
            int layerOutSize = 5;
            for (var i = 0; i < countNeuronLeyers; i++)
            {
                if (i == countNeuronLeyers - 1)
                    layerOutSize = 10;
                addWeightLayer(layerOutSize);
                addBiasLayer();
                if (i == countNeuronLeyers - 1)
                    addActivationLayer(Functions.ReLU);
                else addActivationLayer();
            }
            return new Network(layers);
        }
        
        private void addConvolutionLayer(int[] kernelDims, int depth)
            => layers.AddLast(new ConvolutionLayer(kernelDims, depth, LastLayerOutDims));

        private void addNormalizationLayer()
            => layers.AddLast(new NormalizationLayer(Functions.ReLU, LastLayerOutDims));

        private void addPoolingLayer(int[] poolerDims)
            => layers.AddLast(new PoolingLayer(poolerDims, LastLayerOutDims));

        private void addWeightLayer(int outSize)
            => layers.AddLast(new WeightLayer(LastLayerOutSize, outSize));

        private void addBiasLayer()
            => layers.AddLast(new BiasLayer(LastLayerOutSize));

        private void addActivationLayer()
            => layers.AddLast(new ActivationLayer(LastLayerOutSize, Functions.tanh));

        private void addActivationLayer(FuncDD func)
            => layers.AddLast(new ActivationLayer(LastLayerOutSize, func));
    }
}
