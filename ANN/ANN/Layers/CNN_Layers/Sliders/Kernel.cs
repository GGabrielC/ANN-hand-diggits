using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtensionMethods;
using GlobalItems;
using Utils;

namespace Sliders
{
    public class Kernel:Slider
    {
        public override int DimensionCount => this.weights.DimensionCount; 
        public override int[] Dimensions => this.weights.Dimensions;
        public override int[] Stride => this.stride;

        public MultiMatrix FilterMatrix => weights;

        private MultiMatrix weights;
        private int[] stride;

        public Kernel(int[] dimensions)
        {
            this.weights = new MultiMatrix(dimensions);
            this.stride = ArrayBuilder.repeat(1, this.DimensionCount);
        }

        public Kernel(int[] dimensions, int[] stride)
        {
            this.weights = new MultiMatrix(dimensions);
            this.stride = stride.ShallowCopy();
        }

        public Kernel(Kernel filter)
        {
            this.weights = new MultiMatrix(filter.weights);
            this.stride = ArrayBuilder.repeat(1, this.DimensionCount);
        }

        public override void applyAt(MultiMatrix inData, MultiMatrix outData, int[] coords)
            => outData.setAt(coords, getSumAt(inData, coords));
        
        public override int[] getOutputDims(MultiMatrix input)
            => getOutputDims(input.Dimensions);

        public override int[] getOutputDims(int[] inputDimensions)
        {
            var outputDims = inputDimensions.ShallowCopy();
            for (int i = 0; i < outputDims.Length; i++)
                outputDims[i] -= inputDimensions[i] / 2; // division by 2 expects odd number
            return outputDims;
        }
        
        public override void backwardLearn(MultiMatrix inData, MultiMatrix gradient, double learnRate)
        {
            var gradientW = gradientWeights(inData, gradient);
            this.weights.Data.changeWith(gradientW.Data, (w, g) => w-learnRate*g);
        }

        public override MultiMatrix getGradientInput(MultiMatrix inData, MultiMatrix gradient)
            => gradientInput(gradient);

        public double getSumAt(MultiMatrix inData, int[] coord)
        {
            var sum = 0.0;
            foreach (var offset in this.weights.AllCoords())
            {
                var nearbyCoord = coord.addTo(offset);
                if (inData.areValidCoords(nearbyCoord))
                    sum += inData.at(coord.addTo(offset)) * weights.at(offset);
            }
            return sum;
        }

        public MultiMatrix gradientInput(MultiMatrix gradient)
        {
            var correlationMatrix = new Kernel(this);
            correlationMatrix.rotate90();
            return correlationMatrix.slideOver(gradient.padded(this.Dimensions.addEachTo(-1)));
        }// TODO check ?

        public MultiMatrix gradientWeights(MultiMatrix inData, MultiMatrix gradient)
        {
            var gradientW = new MultiMatrix(this.Dimensions);
            foreach (var coord in gradient.AllCoords())
                foreach (var offset in this.weights.AllCoords())
                {
                    var nearbyCoord = coord.addTo(offset);
                    gradientW.addAt(offset, gradient.at(coord) * inData.at(nearbyCoord));
                }
            return gradientW;
        }
        
        private void rotate90()
            => this.weights.Data.Reverse();
    }
}
