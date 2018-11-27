using ANN;
using ExtensionMethods;
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
    public class MNISTNetworkBuilder
    {
        const int IMAGE_HEIGHT = 28;
        const int IMAGE_WIDTH = 28;
        const int COUNT_LABELS = 10;

        LinkedList<Layer> layers = new LinkedList<Layer>();

        private int[] LastLayerOutDims => ((ConvNetLayer)(layers.Last())).OutDims;
        private int LastLayerOutSize => layers.Last().OutSize;

        public MNISTNetworkBuilder()
        {}

        public Network build()
        {
            var inDims = new int[] {IMAGE_HEIGHT, IMAGE_WIDTH };
            var depth = 8;

            layers.AddLast(new ConvolutionLayer(new int[] { 3, 3 }, depth, inDims));
            LastLayerOutDims.print();// d, 26, 26
            addConvolutionLayer(new int[] { depth, 3, 3 }, depth);
                LastLayerOutDims.print();// d, 1, 24, 24
            addNormalizationLayer();
                LastLayerOutDims.print();// d, 1, 24, 24
            addPoolingLayer(new int[] { depth, 1, 2, 2 });
                LastLayerOutDims.print();// 1, 1, 12, 12

            addConvolutionLayer(new int[] { 1,1, 3, 3 }, depth);
                LastLayerOutDims.print();// d, 1, 1, 10, 10
            addConvolutionLayer(new int[] { depth, 1, 1, 3, 3 }, depth);
                LastLayerOutDims.print();// d, 1, 1, 1, 8, 8
            addNormalizationLayer();
                LastLayerOutDims.print();// d, 1, 1, 1, 8, 8
            addPoolingLayer(new int[] { depth, 1, 1, 1, 2, 2 });
                LastLayerOutDims.print();// 1, 1, 1, 1, 4, 4
            
            int countNeuronLeyers = 10;
            int layerOutSize = 10;
            FuncDD activationFunc = Functions.xOverModXplus1;
            for (var i = 0; i < countNeuronLeyers; i++)
            {
                if (i == countNeuronLeyers - 1)
                {
                    layerOutSize = 10;
                    activationFunc = Functions.sigmoid;
                }
                addWeightLayer(layerOutSize);
                addBiasLayer();
                addActivationLayer(activationFunc);
            }
            return new Network(layers);
        }
        
        private void addConvolutionLayer(int[] kernelDims, int depth)
            => layers.AddLast(new ConvolutionLayer(kernelDims, depth, LastLayerOutDims));

        private void addNormalizationLayer(FuncDD f)
            => layers.AddLast(new NormalizationLayer(f, LastLayerOutDims));

        private void addNormalizationLayer()
            => layers.AddLast(new NormalizationLayer(Functions.ReLU, LastLayerOutDims));

        private void addPoolingLayer(int[] poolerDims)
            => layers.AddLast(new PoolingLayer(poolerDims, LastLayerOutDims));

        private void addWeightLayer(int outSize)
            => layers.AddLast(new WeightLayer(LastLayerOutSize, outSize));

        private void addBiasLayer()
            => layers.AddLast(new BiasLayer(LastLayerOutSize));
        
        private void addActivationLayer(FuncDD func)
            => layers.AddLast(new ActivationLayer(LastLayerOutSize, func));
    }
}
