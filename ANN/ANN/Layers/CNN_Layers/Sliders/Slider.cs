using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Sliders
{
    public abstract class Slider
    {
        public abstract int DimensionCount { get; }
        public abstract int[] Dimensions { get; }
        public abstract int[] Stride { get; }
        
        public MultiMatrix slideOver(MultiMatrix inData)
        {
            var outData = new MultiMatrix(getOutputDims(inData));
            foreach (var coords in outData.AllCoords(this.Stride))
                applyAt(inData, outData, coords);
            return outData;
        }

        public abstract void applyAt(MultiMatrix inData, MultiMatrix outData, int[] coords);
        public abstract int[] getOutputDims(MultiMatrix input);
        public abstract int[] getOutputDims(int [] inputDimensions);

        public abstract MultiMatrix getGradientInput(MultiMatrix inData, MultiMatrix gradient);
        public abstract void backwardLearn(MultiMatrix inData, MultiMatrix gradient, double learnRate);
    }
}
