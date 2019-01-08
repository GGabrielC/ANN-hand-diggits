using ANN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10x_y
{
    public class net11Builder: NetworkBuilder
    {
        public net11Builder() { }

        public override Network build()
        {
            addWeightLayer(1,1);
            return new Network(this.layers);
        }
    }
}
