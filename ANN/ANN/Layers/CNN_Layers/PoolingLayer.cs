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
    public class PoolingLayer : Layer
    {
        public int InSize => inSize;
        public int OutSize => outSize;

        int[] inDimensions => throw new NotImplementedException();
        Pooler pooler;

        int inSize;
        int outSize;

        public PoolingLayer(int[] sliderDimensions, int inSize)
        {
            this.inSize = inSize;
            outSize = pooler.getOutputDims(inDimensions).product();
            this.pooler = new Pooler(sliderDimensions);
        }

        public MatrixD forward(MatrixD inputs)
            => forward(inputs.toMultiMatrix(this.inDimensions)).toMatrixD();
        
        private MultiMatrix forward(MultiMatrix inputs)
            => pooler.slideOver(inputs);

        private MultiMatrix[] forward(MultiMatrix[] inputs)
            => inputs.map(mm => forward(mm.copy()));

        public MatrixD backward(MatrixD inputs, MatrixD gradients)
            => backward(inputs.toMultiMatrix(this.inDimensions),
                        gradients.toMultiMatrix(this.inDimensions)
                        ).toMatrixD();

        private MultiMatrix[] backward(MultiMatrix[] inputs, MultiMatrix[] gradients)
            => inputs.mapWith(gradients, (i,g) => backward(i, g) );

        private MultiMatrix backward(MultiMatrix input, MultiMatrix gradient)
            => pooler.getGradientInput(input, gradient);

        public void backwardLearn(MatrixD inputs, MatrixD gradient, double learnRate)
        { }
    }
}
