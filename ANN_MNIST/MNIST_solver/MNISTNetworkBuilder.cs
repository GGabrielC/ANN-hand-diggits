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
    public class MNISTNetworkBuilder: NetworkBuilder
    {
        const int IMAGE_HEIGHT = 28;
        const int IMAGE_WIDTH = 28;
        const int COUNT_LABELS = 10;
        readonly int[] NETWORK_IN_DIMS = new int[] { IMAGE_HEIGHT, IMAGE_WIDTH };

        public MNISTNetworkBuilder()
        {}

        public override Network build()
        {
            var depth = 5;
            
            addConvolutionLayer(new int[] { 3, 3 }, depth, NETWORK_IN_DIMS);
            addConvolutionLayer(new int[] { depth, 3, 3 }, depth);
            addNormalizationLayer(); 
            addPoolingLayer(new int[] { depth, 1, 2, 2 });

            addConvolutionLayer(new int[] { 1,1, 3, 3 }, depth);
            addConvolutionLayer(new int[] { depth, 1, 1, 3, 3 }, depth);
            addNormalizationLayer();
            addPoolingLayer(new int[] { depth, 1, 1, 1, 2, 2 });
            
            for (var i = 0; i < 9; i++)
                addNeuronLayer(5, Functions.xOverModXplus1);
            addNeuronLayer(10, Functions.sigmoid);
            return new Network(layers);
        }
    }
}
