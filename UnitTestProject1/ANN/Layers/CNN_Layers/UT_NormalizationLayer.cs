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

namespace UT_Layers
{
    [TestClass]
    public class UT_NormalizationLayer
    {
        [TestMethod]
        public void forward()
        {
            var inputs = MatrixD.Build.DenseOfArray(
                new double[,] { { -100, -1, 0, 1, 100, 3 }, { -500, -0.1, 0, 0.1, 500, 3 } });

            var expectedOutputs = MatrixD.Build.DenseOfArray(
                new double[,] { { 0, 0, 0, 1, 100, 3 }, { 0, 0, 0, 0.1, 500, 3 } });

            var layer = new NormalizationLayer(new int[] {6});
            Assert.IsTrue(layer.forward(inputs).EEquals(expectedOutputs));
        }

        [TestMethod]
        public void backward()
        {
            var count_examples = 2;
            var layerInDims = new int[]{6};
            var layerInSize = layerInDims.product();
            var layer = new NormalizationLayer(layerInDims);
            var inputs = MatrixD.Build.DenseOfArray(
                new double[,] { { -100, -1, 0, 1, 100, 3 }, { -500, -0.1, 0, 0.1, 500, 3 } });

            var nextGradients = MatrixD.Build.DenseOfArray(
                new double[,] { { 0, 0, 0, 1, 100,3  }, { 0, 0, 0, 0.1, 500, 3 } });

            var expectedGradients = MatrixD.Build.repeat(count_examples, layerInSize, 0);
            FuncDD df = layer.DerivateActivationFunc;
            for (var i = 0; i < count_examples; i++)
                for (var j = 0; j < layerInSize; j++)
                    expectedGradients[i, j] = nextGradients[i, j] * df(inputs[i, j]);
            
            Assert.IsTrue(layer.backward(inputs, nextGradients).EEquals(expectedGradients));
        }
    }
}
