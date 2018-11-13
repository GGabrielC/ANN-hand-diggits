using ExtensionMethods;
using Layers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatrixD = MathNet.Numerics.LinearAlgebra.Matrix<System.Double>;
using FuncDD = System.Func<System.Double, System.Double>;
using Utils;

namespace UT_Layers
{
    [TestClass]
    public class UT_ActivationLayer
    {
        [TestMethod]
        public void forward()
        {
            var inputs = MatrixD.Build.DenseOfArray(
                new double[,] { { -100, -1, 0, 1, 100 }, { -500, -0.1, 0, 0.1, 500 } });

            var expectedOutputs = MatrixD.Build.DenseOfArray(
                new double[,] { { 0, 0, 0, 1, 100 }, { 0, 0, 0, 0.1, 500 } });

            var layer = new ActivationLayer(5);
            Assert.IsTrue(layer.forward(inputs).EEquals(expectedOutputs));
        }

        [TestMethod]
        public void backward()
        {
            int count_examples = 2;
            int layerInSize = 5;
            var layer = new ActivationLayer(layerInSize);
            var inputs = MatrixD.Build.DenseOfArray(
                new double[,] { { -100, -1, 0, 1, 100 }, { -500, -0.1, 0, 0.1, 500 } });

            var nextGradients = MatrixD.Build.DenseOfArray(
                new double[,] { { 0, 0, 0, 1, 100 }, { 0, 0, 0, 0.1, 500 } });

            var expectedGradients = MatrixD.Build.repeat(count_examples, layerInSize, 0);
            FuncDD[] df = layer.DerivateActivations;
            for (var i = 0; i < count_examples; i++)
                for (var j = 0; j < layerInSize; j++)
                    expectedGradients[i, j] = nextGradients[i, j]*df[j](inputs[i, j]);
            
            Assert.IsTrue(layer.backward(inputs, nextGradients).EEquals(expectedGradients));
        }
    }
}
