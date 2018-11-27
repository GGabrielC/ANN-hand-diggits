using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtensionMethods;
using GlobalRandom_;
using MultiMatrix_;
using Utils;

namespace Sliders
{
    // gradients only for stride 1
    public class Kernel:Slider
    {
        public override int DimensionCount => this.weights.DimensionCount; 
        public override int[] Dimensions => this.weights.Dimensions;
        public override int[] Stride => this.stride;

        public MultiMatrix Weights => weights;

        private MultiMatrix weights;
        private int[] stride;

        public Kernel(int[] dimensions)
        {
            this.weights = MultiMatrix.Build.random(dimensions,-1,1);
            this.stride = ArrayBuilder.repeat(1, this.DimensionCount);
        }

        public Kernel(int[] dimensions, int[] stride)
        {
            this.weights = MultiMatrix.Build.random(dimensions,-1,1);
            this.stride = stride.ShallowCopy();
        }

        public Kernel(int[] dimensions, double[] weights)
        {
            this.weights = new MultiMatrix(dimensions, weights);
            this.stride = ArrayBuilder.repeat(1, this.DimensionCount);
        }

        public Kernel(int[] dimensions, double[] weights, int[] stride)
        {
            this.weights = new MultiMatrix(dimensions, weights);
            this.stride = stride.ShallowCopy();
        }

        public Kernel(MultiMatrix weights)
        {
            this.weights = weights.copy();
            this.stride = ArrayBuilder.repeat(1, this.DimensionCount);
        }

        public Kernel(MultiMatrix weights, int[] stride)
        {
            this.weights = weights.copy();
            this.stride = stride.ShallowCopy();
        }

        public Kernel(Kernel filter)
        {
            this.weights = new MultiMatrix(filter.weights);
            this.stride = filter.Stride.ShallowCopy();
        }

        public override MultiMatrix slideOver(MultiMatrix inData)
        {
            var outData = new MultiMatrix(getOutputDims(inData));
            foreach (var coords in outData.AllCoords())
                outData.setAt(coords, getSumAt(inData, coords.mapWith(Stride, (c, s) => c * s)));
            return outData;
        }
        
        public override void backwardLearn(MultiMatrix inData, MultiMatrix nextGradient, double learnRate)
        {
            var gradientW = getGradientWeights(inData, nextGradient);
            this.weights.Data.changeWith(gradientW.Data, (w, g) => w-learnRate*g);
        }

        public override MultiMatrix getGradientInput(MultiMatrix inData, MultiMatrix nextGradient)
        {
            var gradientIn = MultiMatrix.Build.repeat(inData.Dimensions, 0);
            foreach (var coords in nextGradient.AllCoords())
                foreach (var offset in Weights.AllCoords())
                    gradientIn.addAt(coords.add(offset), nextGradient.at(coords) * Weights.at(offset));
            return gradientIn;
        }

        public double getSumAt(MultiMatrix inData, int[] coord)
        {
            var sum = 0.0;
            foreach (var offset in this.weights.AllCoords())
            {
                var nearbyCoord = coord.add(offset);
                if (inData.areValidCoords(nearbyCoord))
                    sum += inData.at(coord.add(offset)) * weights.at(offset);
            }
            return sum;
        }

        public MultiMatrix getGradientInputWithRotation(MultiMatrix nextGradient)
        {
            var correlationMatrix = new Kernel(this);
            correlationMatrix.rotate90();
            return correlationMatrix.slideOver(nextGradient.padded(this.Dimensions.add(-1)));
        }// TODO check ?
        
        public MultiMatrix getGradientWeights(MultiMatrix inData, MultiMatrix nextGradient)
        {
            var gradientW = new MultiMatrix(this.Dimensions);
            foreach (var coord in nextGradient.AllCoords())
                foreach (var offset in this.weights.AllCoords())
                {
                    var nearbyCoord = coord.add(offset);
                    gradientW.addAt(offset, nextGradient.at(coord) * inData.at(nearbyCoord));
                }
            return gradientW;
        }
        
        private void rotate90()
            => this.weights.Data.Reverse();
    }
}
