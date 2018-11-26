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
using GlobalRandom_;

namespace UT_Layers
{
    [TestClass]
    public class UT_BiasLayer
    {
        [TestMethod]
        public void forward()
        {
            var countExamples = 3;
            var inSize = 5;
            var input = MatrixD.Build.Random(countExamples, inSize);

            var layer = new BiasLayer(inSize);
            var expectedOutputs = MatrixD.Build.repeat(countExamples, inSize, 0);
            for (var i = 0; i < countExamples; i++)
                for (var j = 0; j < inSize; j++)
                    expectedOutputs[i,j] = input[i,j] + layer.Biases[j];
            
            Assert.IsTrue(layer.forward(input).EEquals(expectedOutputs));
        }

        [TestMethod]
        public void backward()
        {
            int inSize, outSize;
            var countExamples = 3;
            inSize = outSize = 5;

            var input = MatrixD.Build.Random(countExamples, inSize);
            var layer = new BiasLayer(inSize);
            var nextGradients = MatrixD.Build.Random(countExamples, outSize);
            Assert.IsTrue(layer.backward(input, nextGradients).EEquals(nextGradients));
        }

        [TestMethod]
        public void backwardLearn()
        {
            var learnRate = GlobalRandom.NextDouble();
            int inSize, outSize;
            var countExamples = 3;
            inSize = outSize = 5;

            var input = MatrixD.Build.Random(countExamples, inSize);
            var layer = new BiasLayer(inSize);
            var nextGradients = MatrixD.Build.Random(countExamples, outSize);

            var expectedBiases = layer.Biases.ShallowCopy();
            var gradientSums = nextGradients.ColumnSums().AsArray();
            for (int i = 0; i < expectedBiases.Length; i++)
                expectedBiases[i] -= learnRate*gradientSums[i];
            
            layer.backwardLearn(input, nextGradients, learnRate);
            Assert.IsTrue(expectedBiases.EEquals(layer.Biases));
        }
    }
}

