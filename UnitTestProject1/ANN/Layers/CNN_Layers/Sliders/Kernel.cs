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
    public class UT_Kernel
    {
        [TestMethod]
        public void slideOver()
        {
            var weights = MultiMatrix.Build.repeat(new int[] { 3, 3 }, 1);
            var kernel = new Kernel(weights);

            var inData = new MultiMatrix(new int[] { 5, 5 }, new double[] {
                0, 0,0,0, 0,
                0, 1,2,3, 0,
                0, 4,5,6, 0,
                0, 7,8,9, 0,
                0, 0,0,0, 0,
            });
            
            var expectedOutput = new MultiMatrix(new int[] { 3, 3 }, new double[] {
               12, 21, 16,
               27, 45, 33,
               24, 39, 28,
            });
            
            var output = kernel.slideOver(inData);
            Assert.IsTrue(output.EEquals(expectedOutput));
        }
        
        [TestMethod]
        public void getSumAt()
        {
            var dimensions = new int[] { 5, 5, 5 };
            var data = MultiMatrix.Build.random(dimensions);
            var kernel = new Kernel(new int[] { 3, 3, 3 });
            var coordTarget = new int[] { 1, 1, 1 };
            var expectedSum = 0.0;
            foreach (var offset in kernel.Weights.AllCoords())
                expectedSum += kernel.Weights.at(offset) * data.at(coordTarget.add(offset));
            var sum = kernel.getSumAt(data, coordTarget);
            Assert.IsTrue(sum.EEquals(expectedSum));

            var hardcodedData = new MultiMatrix(new int[] { 5, 5 }, new double[] {
                -100,-100,-100,-100,-100,
                -100, 1,2,3, -100,
                -100, 4,5,6, -100,
                -100, 7,8,9, -100,
                -100,-100,-100,-100,-100,});
            var hardcodedWeights = new double[]{ 2, 4, 6, 8, 10, 12, 14, 16, 18 };
            var hardcodedKernel = new Kernel(new MultiMatrix(new int[] {3,3}, hardcodedWeights));
            var theSum = hardcodedKernel.getSumAt(hardcodedData, new int[] { 1, 1 });
            var eSum= 1 * 2 + 2 * 4 + 3 * 6 + 4 * 8 + 5 * 10 + 6 * 12 + 7 * 14 + 8 * 16 + 9 * 18;
            Assert.IsTrue(theSum.EEquals(eSum));
        }
        
        [TestMethod]
        public void getGradientInput()
        {
            var inData = new MultiMatrix(new int[] { 3, 3 }, new double[] {
                1,2,3,
                4,5,6,
                7,8,9,
            });

            var weights = new double[] {
                1,2,
                3,4,
            };
            var gradientNext = new MultiMatrix(new int[] { 2, 2 }, new double[] {
                1,-1,
                2, 3,
            });

            var kernel = new Kernel(new int[] { 2, 2 }, weights);
            var expectedGradient = new MultiMatrix(new int[] { 3, 3 }, new double[] {
                1, 1,-2,
                5, 8, 2,
                6,17, 12,
            });

            var gradient = kernel.getGradientInput(inData, gradientNext);
            Assert.IsTrue(expectedGradient.EEquals(gradient));
        }

        [TestMethod]
        public void getGradientWeights()
        {
            var inData = new MultiMatrix(new int[] { 3, 3 }, new double[] {
                1,2,3,
                4,5,6,
                7,8,9,
            });

            var weights = new double[] { 1, 2, 3, 4 };
            var gradientNext = new MultiMatrix(new int[] { 2, 2 }, new double[] {
                3,-1,
                2, 1,
            });

            var kernel = new Kernel(new int[] { 2, 2 }, weights);
            var expectedGradient = new MultiMatrix(new int[] { 2, 2}, new double[] {
                14, 19,
                29, 34,
            });

            var gradient = kernel.getGradientWeights(inData, gradientNext);
            Assert.IsTrue(expectedGradient.EEquals(gradient));
        }

        [TestMethod]
        public void backwardLearn()
        {
            var inData = new MultiMatrix(new int[] { 3, 3 }, new double[] {
                1,2,3,
                4,5,6,
                7,8,9,
            });

            var weights = new double[] { 1, 2, 3, 4 };
            var gradientNext = new MultiMatrix(new int[] { 2, 2 }, new double[] {
                3,-1,
                2, 1,
            });

            var learnRate = 0.5;
            var kernel = new Kernel(new int[] { 2, 2 }, weights);
            var gradientW = kernel.getGradientWeights(inData, gradientNext);
            var expectedWeights = kernel.Weights.add(gradientW.scalarMultiply(-learnRate));

            kernel.backwardLearn(inData, gradientNext, learnRate);
            Assert.IsTrue(kernel.Weights.EEquals(expectedWeights));
        }

        [TestMethod]
        public void getOutputDims()
        {
            var inDims = new int[] { 2, 3, 4, 5 };
            var kDims = new int[] { 2, 2, 2, 2 };
            var kStrides = kDims;
            var outDims = new Kernel(kDims, kStrides).getOutputDims(inDims);
            var expectedOutDims = new int[] { 1, 2, 2, 3 };
            Assert.IsTrue(expectedOutDims.EEquals(outDims));

            inDims = new int[] { 3, 4, 5, 6, 3 };
            kDims = new int[] { 2, 3, 2, 4, 3 };
            kStrides = kDims;
            outDims = new Kernel(kDims, kStrides).getOutputDims(inDims);
            expectedOutDims = new int[] { 2, 2, 3, 2, 1 };
            Assert.IsTrue(expectedOutDims.EEquals(outDims));

            inDims = new int[] { 3, 4, 5, 6, 3 };
            kDims = new int[] { 2, 3, 2, 4, 3 };
            kStrides = new int[] { 1, 1, 1, 1, 1 };
            outDims = new Kernel(kDims, kStrides).getOutputDims(inDims);
            expectedOutDims = new int[] { 2, 2, 4, 3, 1 };
            Assert.IsTrue(expectedOutDims.EEquals(outDims));

            inDims = new int[] { 3, 4, 5, 6, 3 };
            kDims = new int[] { 2, 3, 2, 4, 3 };
            outDims = new Kernel(kDims).getOutputDims(inDims);
            expectedOutDims = new int[] { 2, 2, 4, 3, 1 };
            Assert.IsTrue(expectedOutDims.EEquals(outDims));
        }
    }
}