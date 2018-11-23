using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatrixD = MathNet.Numerics.LinearAlgebra.Matrix<System.Double>;
using FuncDD = System.Func<System.Double, System.Double>;
using Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Layers;
using ExtensionMethods;
using Global;
using Sliders;

namespace UT_Layers
{
    [TestClass]
    public class UT_ConvolutionLayer
    {
        int depth;
        double learnRate = 0.5;
        int countEntries;
        int entrySize;
        int[] inDims;
        MatrixD entries;
        MatrixD expectedOutputs;
        MatrixD nextGradients;
        MatrixD expectedGradients;
        MultiMatrix[] expectedLearnedWeights;
        Kernel[] kernels;
        ConvolutionLayer layer;

        [TestInitializeAttribute]
        public void setup()
        {
            this.depth = GlobalRandom.NextInt(2, 5);
            this.countEntries = GlobalRandom.NextInt(2, 5);
            this.inDims = GlobalRandom.NextIntArr(countEntries, 2, 5);
            this.entrySize = inDims.product();
            this.kernels = ArrayBuilder.repeat( 
                new Kernel(inDims.map(x => GlobalRandom.NextInt(2, x))), depth);
            this.layer = new ConvolutionLayer(this.kernels, this.inDims);
            MultiMatrix[] entries = ArrayBuilder.repeat(() => MultiMatrix.Build.random(inDims), countEntries);
            MultiMatrix[][] expectedOutputs = new MultiMatrix[countEntries][];
            MultiMatrix[][] nextGradients = new MultiMatrix[countEntries][];
            MultiMatrix[] expectedInGradients = new MultiMatrix[countEntries];
            Kernel[] kerns = kernels.map(k=>new Kernel(k));
            for (int i = 0; i < countEntries; i++)
            {
                expectedInGradients[i] = MultiMatrix.Build.repeat(inDims, 0);
                expectedOutputs[i] = new MultiMatrix[depth];
            }
            for (int i = 0; i < countEntries; i++)
            {
                nextGradients[i] = new MultiMatrix[depth];
                for (var j = 0; j < kerns.Length; j++)
                {
                    expectedOutputs[i][j] = kernels[j].slideOver(entries[i]);
                    nextGradients[i][j] = MultiMatrix.Build.random(kernels[0].getOutputDims(inDims));
                    expectedInGradients[i] += kernels[j].getGradientInput(entries[i], nextGradients[i][j]);
                    kerns[j].backwardLearn(entries[i], nextGradients[i][j], learnRate);
                }
            }
            this.entries = entries.toMatrixD();
            this.expectedOutputs = expectedOutputs.map(o=>new MultiMatrix(o)).toMatrixD();
            this.nextGradients = nextGradients.map(g => new MultiMatrix(g)).toMatrixD();
            this.expectedGradients = expectedInGradients.toMatrixD();
            this.expectedLearnedWeights = kerns.map(k => k.Weights);
        }

        [TestMethod]
        public void forward()
        {
            var outputs = layer.forward(entries);
            Assert.IsTrue(outputs.EEquals(expectedOutputs));
        }

        [TestMethod]
        public void backward()
        {
            var gradients = layer.backward(entries, nextGradients);
            Assert.IsTrue(gradients.EEquals(expectedGradients));
        }

        [TestMethod]
        public void backwardLearn()
        {
            var layer = new ConvolutionLayer(this.layer);
            layer.backwardLearn(entries, nextGradients, learnRate);
            var weights = layer.Kernels.map(k => k.Weights);
            for(int i=0; i<expectedLearnedWeights.Length; i++)
                Assert.IsTrue(expectedLearnedWeights[i].EEquals(weights[i]));
        }
    }
}
