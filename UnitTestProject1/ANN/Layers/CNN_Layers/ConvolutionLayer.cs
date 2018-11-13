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

namespace UT_Layers
{
    [TestClass]
    public class UT_ConvolutionLayer
    {
        [TestMethod]
        public void forward()
        {
            Assert.IsTrue(false);
        }

        [TestMethod]
        public void backward()
        {
            Assert.IsTrue(false);
        }

        public void gradientWeights()
        {
            Assert.IsTrue(false);
        }

        [TestMethod]
        public void backwardLearn()
        {
            Assert.IsTrue(false);
        }
    }
}
