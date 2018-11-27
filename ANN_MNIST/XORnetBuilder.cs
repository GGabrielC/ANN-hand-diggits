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
    public class XORnetBuilder
    {
        const int netInSize = 2;
        const int netOutSize = 1;

        LinkedList<Layer> layers = new LinkedList<Layer>();
        private int[] LastLayerOutDims => ((ConvNetLayer)(layers.Last())).OutDims;
        private int LastLayerOutSize => layers.Last().OutSize;

        public XORnetBuilder()
        { }

        public Network build()
        {
            addNeuron(netInSize, 3, Functions.ReLU);
            addNeuron(1, Functions.ReLU);

            return new Network(layers);
        }

        private void addNeuron(int outSize, FuncDD activationFunc)
        {
            addWeightLayer(LastLayerOutSize, outSize);
            addBiasLayer();
            addActivationLayer(activationFunc);
        }

        private void addNeuron( int inSize, int outSize, FuncDD activationFunc)
        {
            addWeightLayer(inSize, outSize);
            addBiasLayer();
            addActivationLayer(activationFunc);
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

        private void addWeightLayer(int inSize, int outSize)
            => layers.AddLast(new WeightLayer(inSize, outSize));

        private void addBiasLayer()
            => layers.AddLast(new BiasLayer(LastLayerOutSize));

        private void addActivationLayer(FuncDD func)
            => layers.AddLast(new ActivationLayer(LastLayerOutSize, func));
    }
}
