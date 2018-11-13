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

        public abstract MultiMatrix slideOver(MultiMatrix inData);

        public abstract MultiMatrix getGradientInput(MultiMatrix inData, MultiMatrix gradient);
        public abstract void backwardLearn(MultiMatrix inData, MultiMatrix gradient, double learnRate);

        public int[] getOutputDims(MultiMatrix input)
            => getOutputDims(input.Dimensions);

        public int[] getOutputDims(int[] inDims)
        {
            var outputDims = new int[this.DimensionCount];
            for (int i = 0; i < outputDims.Length; i++)
                outputDims[i] = (int)Math.Ceiling((inDims[i]-Dimensions[i])/(double)Stride[i])+1;
            return outputDims;
        }

    }
}
