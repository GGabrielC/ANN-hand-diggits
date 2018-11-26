using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ANN;
using ExtensionMethods;
using LayerInputExtensions;
using MathNet.Numerics.LinearAlgebra;
using MultiMatrix_;
using Sliders;
using Utils;
using MatrixD = MathNet.Numerics.LinearAlgebra.Matrix<System.Double>;

namespace Layers
{
    public class ConvolutionLayer : Layer, ConvNetLayer
    {
        public int InSize => inSize;
        public int OutSize => outSize;
        public int [] KernelDims => kernels[0].Dimensions;
        public int KernelDimCount => kernels[0].Dimensions.Count();
        
        public Kernel[] Kernels
        {
            get => kernels.map(k => new Kernel(k));
            private set
            {
                var kernelDims = value[0].Dimensions;
                if(value.Count() == 1)
                    kernels = value.map(k=> new Kernel(k));
                else
                {
                    if (!value.All(x => x.Dimensions.EEquals(kernelDims)))
                        throw new Exception("All kernels must have same dimensions!");
                    kernels = value.map(k => new Kernel(k));
                }
                OutDims = getOutputDims(inDims);
            }
        }

        public int[] InDims
        {
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
                outDims = value.ShallowCopy();
                outSize = outDims.product();
            }
        }

        int inSize;
        int outSize;
        int[] inDims;
        int[] outDims;
        Kernel[] kernels;

        public ConvolutionLayer(Kernel[] kernels, int[] inDims)
            => set(kernels, inDims);

        public ConvolutionLayer(int[] kernelDims, int depth, int[] inDims)
            => set( ArrayBuilder.repeat( () => new Kernel(kernelDims), depth), inDims);

        public ConvolutionLayer(Kernel kernel, int[] inDims)
            => set(new Kernel[] {kernel}, inDims);

        public ConvolutionLayer(ConvolutionLayer layer)
            => set(layer.kernels, layer.inDims);

        public void set(Kernel[] kernels, int[] inDims)
        {
            InDims = inDims;
            Kernels = kernels;
        }
        
        public MatrixD forward(MatrixD entries)
            => forward(entries.toMultiMatrix(this.inDims)).toMatrixD();

        public MultiMatrix[] forward(MultiMatrix[] entries)
            => entries.map( mm => forward(mm) );
        
        public MultiMatrix forward(MultiMatrix entry)
            => new MultiMatrix(kernels.map(k=>k.slideOver(entry)));

        public MatrixD backward(MatrixD entries, MatrixD nextGradients)
            => backward(entries.toMultiMatrix(this.inDims),
                        nextGradients.toMultiMatrix(this.outDims)
                        ).toMatrixD();

        public MultiMatrix[] backward(MultiMatrix[] entries, MultiMatrix[] nextGradients)
            => entries.mapWith(nextGradients, (i, g) => backward(i, g));

        public MultiMatrix backward(MultiMatrix entry, MultiMatrix nextGradient)
        {
            MultiMatrix[] gradientInput =
                kernels.mapWith(nextGradient.split(), (k, g) => k.getGradientInput(entry, g));
            return gradientInput.Aggregate(MultiMatrix.Build.repeat(entry.Dimensions, 0),
                                            (sum, el) => sum + el);
        }

        public void backwardLearn(MatrixD entry, MatrixD nextGradient, double learnRate)
            => backwardLearn(entry.toMultiMatrix(this.inDims),
                             nextGradient.toMultiMatrix(this.outDims),
                             learnRate);

        public void backwardLearn(MultiMatrix[] entries, MultiMatrix[] nextGradients, double learnRate)
            => entries.forEachWith(nextGradients, (i, g) => backwardLearn(i, g, learnRate));

        public void backwardLearn(MultiMatrix entry, MultiMatrix nextGradient, double learnRate)
            => kernels.forEachWith(nextGradient.split(), (k,g) => k.backwardLearn(entry,g,learnRate));

        public int[] getOutputDims(int[] inDims)
        {
            if (KernelDimCount == 1)
                return kernels[0].getOutputDims(inDims);
            var outDims = new int[1 + KernelDimCount];
            outDims[0] = kernels.Count();
            kernels[0].getOutputDims(inDims).CopyTo(outDims, 1);
            return outDims;
        }
    }
}
