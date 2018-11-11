using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ANN;
using ExtensionMethods;
using MathNet.Numerics.LinearAlgebra;
using Sliders;
using Utils;
using MatrixD = MathNet.Numerics.LinearAlgebra.Matrix<System.Double>;

namespace Layers
{
    public class ConvolutionLayer : Layer
    {
        public int InSize => inSize;
        public int OutSize => outSize;

        int[] inDimensions => throw new NotImplementedException();
        Kernel[] kernels;

        int inSize;
        int outSize;

        public ConvolutionLayer(int[] sliderDimensions, int depth, int inSize, bool withPadding = false)
        {
            inSize = inSize;
            outSize = kernels.First().getOutputDims(inDimensions).multiplyTo();
            this.kernels = new Kernel[depth];
            for (var i = 0; i < this.kernels.Length; i++)
                this.kernels[i] = new Kernel(sliderDimensions);
        }

        public MatrixD forward(MatrixD inputs)
            => forward(inputs.toMultiMatrix(this.inDimensions)).toMatrixD();
        
        private MultiMatrix[] forward(MultiMatrix[] inputs)
            => inputs.map( mm => forward(mm.copy()) );

        private MultiMatrix forward(MultiMatrix input)
        {
            MultiMatrix[] filteredData = new MultiMatrix[kernels.Length];
            for (int i = 0; i < kernels.Length; i++)
                filteredData[i] = kernels[i].slideOver(input);
            return new MultiMatrix(filteredData);
        }

        public MatrixD backward(MatrixD inputs, MatrixD gradients)
            => backward(inputs.toMultiMatrix(this.inDimensions),
                        gradients.toMultiMatrix(this.inDimensions)
                        ).toMatrixD();

        private MultiMatrix[] backward(MultiMatrix[] inputs, MultiMatrix[] gradients)
            => inputs.mapWith(gradients, (i, g) => backward(i, g));

        private MultiMatrix backward(MultiMatrix input, MultiMatrix gradient)
        {
            MultiMatrix[] gradientInput = new MultiMatrix[kernels.Length];
            for (int i = 0; i < kernels.Length; i++)
                gradientInput[i] = kernels[i].getGradientInput(input, gradient);
            return new MultiMatrix(gradientInput); //TODO sum ?
        }

        public void backwardLearn(MatrixD inputs, MatrixD gradients, double learnRate)
            => backwardLearn(inputs.toMultiMatrix(this.inDimensions),
                             gradients.toMultiMatrix(this.inDimensions),
                             learnRate);

        public void backwardLearn(MultiMatrix[] inputs, MultiMatrix[] gradients, double learnRate)
            => inputs.actionWith(gradients, (i, g) => backwardLearn(i, g, learnRate));

        public void backwardLearn(MultiMatrix input, MultiMatrix gradient, double learnRate)
        {
            foreach (var kernel in this.kernels)
                kernel.backwardLearn(input, gradient, learnRate);
        }
    }
}
