using ANN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace XOR_
{
    public class XORnetBuilder : NetworkBuilder
    {
        const int netInSize = 2;
        const int netOutSize = 1;

        public XORnetBuilder()
        { }

        public override Network build()
        {
            addNeuron(netInSize, 3, Functions.xOverModXplus1);
            addNeuronLayer(netOutSize, Functions.ReLU);

            return new Network(layers);
        }

    }
}
