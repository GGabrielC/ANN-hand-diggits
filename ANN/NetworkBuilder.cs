using Layers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using FuncDD = System.Func<System.Double, System.Double>;

namespace ANN
{
    public abstract class NetworkBuilder
    {
        protected LinkedList<Layer> layers = new LinkedList<Layer>();

        protected int[] LastLayerOutDims => ((ConvNetLayer)(layers.Last())).OutDims;
        protected int LastLayerOutSize => layers.Last().OutSize;

        public abstract Network build();

        protected void addNeuronLayer(int outSize, FuncDD activationFunc)
        {
            addWeightLayer(LastLayerOutSize, outSize);
            addBiasLayer();
            addActivationLayer(activationFunc);
        }
        protected void addNeuron(int inSize, int outSize, FuncDD activationFunc)
        {
            addWeightLayer(inSize, outSize);
            addBiasLayer();
            addActivationLayer(activationFunc);
        }

        protected void addConvolutionLayer(int[] kernelDims, int depth)
            => layers.AddLast(new ConvolutionLayer(kernelDims, depth, LastLayerOutDims));
        protected void addConvolutionLayer(int[] kernelDims, int depth, int[] inDims)
            => layers.AddLast(new ConvolutionLayer(kernelDims, depth, inDims));

        protected void addNormalizationLayer(FuncDD f)
            => layers.AddLast(new NormalizationLayer(f, LastLayerOutDims));
        protected void addNormalizationLayer()
            => layers.AddLast(new NormalizationLayer(Functions.ReLU, LastLayerOutDims));

        protected void addPoolingLayer(int[] poolerDims)
            => layers.AddLast(new PoolingLayer(poolerDims, LastLayerOutDims));

        protected void addWeightLayer(int outSize)
            => layers.AddLast(new WeightLayer(LastLayerOutSize, outSize));
        protected void addWeightLayer(int inSize, int outSize)
            => layers.AddLast(new WeightLayer(inSize, outSize));

        protected void addBiasLayer()
            => layers.AddLast(new BiasLayer(LastLayerOutSize));

        protected void addActivationLayer(FuncDD func)
            => layers.AddLast(new ActivationLayer(LastLayerOutSize, func));
    }
}
