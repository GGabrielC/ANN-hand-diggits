using ANN;
using ExtensionMethods;
using Sliders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using MatrixD = MathNet.Numerics.LinearAlgebra.Matrix<System.Double>;

namespace Layers
{
    public class PoolingLayer : Layer, ConvNetLayer
    {
        public int InSize => inSize;
        public int OutSize => outSize;

        public Pooler Pooler {
            get => pooler;
            private set
            {
                if (value.Dimensions.Length != inDims.Length)
                {
                    var newDims = inDims.ShallowCopy();
                    for (int i = 0, j = 0; i < newDims.Length; i++)
                        if (newDims[i] != 1)
                            newDims[i] = value.Dimensions[j++];
                    pooler = new Pooler(newDims);
                }
                else
                {
                    pooler = value;
                }
                OutDims = pooler.getOutputDims(inDims);
            }
        }

        public int[] InDims {
            get => inDims.ShallowCopy();
            private set
            {
                inDims = value.ShallowCopy();
                inSize = inDims.product();
            }
        }

        public int[] OutDims
        {
            get => outDims.ShallowCopy();
            private set
            {
                if (!pooler.getOutputDims(inDims).EEquals(value))
                    throw new Exception("Output dims do not fit input and pooler!");
                outDims = value.ShallowCopy();
                outSize = outDims.product();
            }
        }

        int[] inDims;
        int[] outDims;
        Pooler pooler;
        int inSize;
        int outSize;

        public PoolingLayer(Pooler pooler, int[] inDimensions)
            => set(pooler, inDimensions);

        public PoolingLayer(int[] sliderDimensions, int[] inDimensions)
            => set(new Pooler(sliderDimensions), inDimensions);

        public void set(Pooler pooler, int[] inDims)
        {
            this.InDims = inDims;
            this.Pooler = pooler;
        }

        public MatrixD forward(MatrixD inputs)
            => forward(inputs.toMultiMatrix(this.InDims)).toMatrixD();
        
        public MultiMatrix forward(MultiMatrix inputs)
            => pooler.slideOver(inputs);

        public MultiMatrix[] forward(MultiMatrix[] inputs)
            => inputs.map(mm => forward(mm.copy()));

        public MatrixD backward(MatrixD inputs, MatrixD gradients)
            => backward(inputs.toMultiMatrix(this.InDims),
                        gradients.toMultiMatrix(this.OutDims)
                        ).toMatrixD();

        public MultiMatrix[] backward(MultiMatrix[] inputs, MultiMatrix[] gradients)
            => inputs.mapWith(gradients, (i,g) => backward(i, g) );

        public MultiMatrix backward(MultiMatrix input, MultiMatrix gradient)
            => pooler.getGradientInput(input, gradient);

        public void backwardLearn(MatrixD inputs, MatrixD gradient, double learnRate)
        { }
    }
}
