using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MatrixD = MathNet.Numerics.LinearAlgebra.Matrix<System.Double>;
using FuncDD = System.Func<System.Double, System.Double>;
using Layers;
using ExtensionMethods;

namespace UT_Layers
{
    [TestClass]
    public class UT_WeightLayer
    {
        [TestMethod]
        public void forward()
        {
            const int inSize = 3;
            const int outSize = 2;
            const int countExamples = 1;

            var input = MatrixD.Build.DenseOfArray(
                new double[countExamples, inSize] { { 1, 2, 3 } });

            var weights = MatrixD.Build.DenseOfArray(
                new double[inSize, outSize] {
                    { -1, 2 },
                    { -3, 2 },
                    { -1, 3 },
                });

            var expectedOutput = MatrixD.Build.DenseOfArray(
                new double[countExamples, outSize]{
                    {-10, 15 }
                });

            var layer = new WeightLayer(weights);
            var output = layer.forward(input);
            Assert.IsTrue(output.EEquals(expectedOutput));
        }

        [TestMethod]
        public void backward()
        {
            const int inSize = 3;
            const int outSize = 2;
            const int countExamples = 1;

            var input = MatrixD.Build.DenseOfArray(
                new double[countExamples, inSize] { { 1, 2, 3 } });

            var nextGradient = MatrixD.Build.DenseOfArray(
                new double[countExamples, outSize] { { 4, 5 } });

            var weights = MatrixD.Build.DenseOfArray(
                new double[inSize, outSize] {
                    { -1, 2 },
                    { -3, 2 },
                    { -1, 3 },
                });

            var expectedGradient = MatrixD.Build.DenseOfArray(
                new double[countExamples, inSize] { { 6, -2, 11 } });

            var layer = new WeightLayer(weights);
            var gradient = layer.backward(input, nextGradient);
            Assert.IsTrue(gradient.EEquals(expectedGradient));
        }

        [TestMethod]
        public void gradientWeights()
        {
            const int inSize = 3;
            const int outSize = 2;
            const int countExamples = 1;

            var input = MatrixD.Build.DenseOfArray(
                new double[countExamples,inSize] { { 1, 2, 3 } });

            var nextGradient = MatrixD.Build.DenseOfArray(
                new double[countExamples, outSize] { { 4, 5 } });

            var weights = MatrixD.Build.DenseOfArray(
                new double[inSize, outSize] {
                    { -1, 2 },
                    { -3, 2 },
                    { -1, 3 },
                });

            var expectedGradient = MatrixD.Build.DenseOfArray(
                new double[inSize, outSize]{
                    { 4,  5 },
                    { 8, 10 },
                    {12, 15 },
                });

            var layer = new WeightLayer(inSize, outSize);
            var gradient = layer.gradientWeights(input, nextGradient);
            Assert.IsTrue(gradient.EEquals(expectedGradient));
        }

        [TestMethod]
        public void backwardLearn()
        {
            const double learnRate = 0.5;
            const int inSize = 3;
            const int outSize = 2;
            const int countExamples = 1;

            var input = MatrixD.Build.DenseOfArray(
                new double[countExamples, inSize] { { 1, 2, 3 } });

            var nextGradient = MatrixD.Build.DenseOfArray(
                new double[countExamples, outSize] { { 4, 5 } });

            var weights = MatrixD.Build.DenseOfArray(
                new double[inSize, outSize] {
                    { -1, 2 },
                    { -3, 2 },
                    { -1, 3 },
                });

            var expectedWeights = MatrixD.Build.DenseOfArray(
                new double[inSize, outSize] {
                    { -3, -0.5 },
                    { -7, -3 },
                    { -7, -4.5 },
                });

            var layer = new WeightLayer(weights);
            layer.backwardLearn(input, nextGradient, learnRate);
            Assert.IsTrue(expectedWeights.EEquals(layer.Weights));
        }
    }
}