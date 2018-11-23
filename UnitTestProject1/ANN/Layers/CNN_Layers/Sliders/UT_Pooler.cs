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
    public class UT_Pooler
    {
        [TestMethod]
        public void slideOver()
        {
            var inData = new MultiMatrix(new int[] { 5, 5 }, new double[] {
                0,  1,   2,  3,  4,
                10, 11,  9, 14, 13,
                20, 24, 25, 26, 20,
                32, 27, 28, 29, 21,
                31, 33, 34, 35, -1,
            });

            var expectedOutput = new MultiMatrix(new int[] { 3, 3 }, new double[] {
               11, 14, 13,
               32, 29, 21,
               33, 35, -1,
            });

            var pooler = new Pooler(new int[] { 2, 2 });
            var output = pooler.slideOver(inData);
            Assert.IsTrue(output.EEquals(expectedOutput));
        }

        [TestMethod]
        public void getMaxAt()
        {
            var inData = new MultiMatrix(new int[] { 5, 5 }, new double[] {
                0, 0, 0, 0, 999,
                0, 1, 2, 3, 0,
                0, 4, 5, 6, 0,
                0, 7, 8, 9, 0,
                0, 0, 0,99, 999,
            });
            
            var pooler = new Pooler(new int[] { 2, 2 });
            Assert.IsTrue(pooler.getMaxAt(inData, new int[] { 0, 0 }).EEquals(1));
            Assert.IsTrue(pooler.getMaxAt(inData, new int[] { 2, 2 }).EEquals(9));
            Assert.IsTrue(pooler.getMaxAt(inData, new int[] { 0, 4 }).EEquals(999));
            Assert.IsTrue(pooler.getMaxAt(inData, new int[] { 4, 2 }).EEquals(99));
            Assert.IsTrue(pooler.getMaxAt(inData, new int[] { 4, 4 }).EEquals(999));
        }

        [TestMethod]
        public void getGradientInput()
        {
            var inData = new MultiMatrix(new int[] { 5, 5 }, new double[] {
                0,  1,   2,  3,  4,
                10, 11,  9, 14, 13,
                20, 24, 25, 26, 20,
                32, 27, 28, 29, 21,
                31, 33, 34, 35, -1,
            });

            var gradientNext = new MultiMatrix(new int[] { 3, 3 }, new double[] {
               1,1,1,
               1,1,1,
               1,1,1,
            });

            var expectedGradient = new MultiMatrix(new int[] { 5, 5 }, new double[] {
                0, 0, 0, 0, 0,
                0, 1, 0, 1, 1,
                0, 0, 0, 0, 0,
                1, 0, 0, 1, 1,
                0, 1, 0, 1, 1,
            });
            
            var pooler = new Pooler(new int[] { 2, 2 });
            var gradient = pooler.getGradientInput(inData, gradientNext);
            Assert.IsTrue(expectedGradient.EEquals(gradient));
        }

        [TestMethod]
        public void getOutputDims()
        {
            var inDims = new int[] { 2, 3, 4, 5 };
            var outDims = new Pooler(new int[] { 2, 2, 2, 2 }).getOutputDims(inDims);
            var expectedOutDims = new int[] { 1, 2, 2, 3 };
            Assert.IsTrue(expectedOutDims.EEquals(outDims));

            inDims = new int[] { 3, 4, 5, 6, 3 };
            outDims = new Pooler(new int[] { 2,3,2,4,3}).getOutputDims(inDims);
            expectedOutDims = new int[] { 2, 2, 3, 2, 1 };
            Assert.IsTrue(expectedOutDims.EEquals(outDims));
        }
    }
}