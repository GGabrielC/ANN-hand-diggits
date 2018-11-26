using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers
{
    public interface ConvNetLayer
    {
        int[] InDims { get; }
        int[] OutDims { get; }
    }
}
