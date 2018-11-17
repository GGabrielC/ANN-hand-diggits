using ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Sliders
{
    public class Pooler : Slider
    {
        public override int DimensionCount => this.poolerMatrix.DimensionCount;
        public override int[] Dimensions => this.poolerMatrix.Dimensions;
        public override int[] Stride => this.Dimensions;

        public MultiMatrix PoolerMatrix { get => poolerMatrix; }

        private MultiMatrix poolerMatrix;

        public Pooler(int[] dimensions)
            => this.poolerMatrix = new MultiMatrix(dimensions);

        public override MultiMatrix slideOver(MultiMatrix inData)
        {
            var outData = new MultiMatrix(getOutputDims(inData));
            foreach (var coords in outData.AllCoords())
                outData.setAt(coords, getMaxAt(inData, coords.mapWith(Stride, (c, s) => c * s)));

            return outData;
        }

        public override MultiMatrix getGradientInput(MultiMatrix inData, MultiMatrix gradient)
        {
            var gradientInput = new MultiMatrix(inData.Dimensions);

            foreach (var coords in inData.AllCoords(this.Stride))
            {
                var max = getMaxAt(inData, coords);
                foreach (var offset in poolerMatrix.AllCoords())
                {
                    var nearbyCoord = coords.add(offset);
                    if (inData.areValidCoords(nearbyCoord))
                        if (max.EEquals(inData.at(nearbyCoord)))
                            gradientInput.setAt(nearbyCoord, gradient.at(coords.mapWith(Dimensions, (c, d) => c / d)));
                        else gradientInput.setAt(nearbyCoord, 0);
                }
            }
            return gradientInput;
        }

        public override void backwardLearn(MultiMatrix inData, MultiMatrix gradient, double learnRate)
        { }

        public double getMaxAt(MultiMatrix inData, int[] coords)
        {
            var max = double.MinValue;
            foreach (var offset in poolerMatrix.AllCoords())
            {
                var nearbyCoord = coords.add(offset);
                if (inData.areValidCoords(nearbyCoord))
                    max = Math.Max(max, inData.at(nearbyCoord));
            }
            return max;
        }

        public override string ToString()
            => PoolerMatrix.ToString();
    }
}
