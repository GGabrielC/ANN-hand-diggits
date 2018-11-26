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
using Sliders;
using MultiMatrix_;
using LayerInputExtensions;

namespace UT_Layers
{
    [TestClass]
    public class UT_PoolingLayer
    {
        int countEntries;
        int entrySize;
        int[] inDims;
        MatrixD inputs;
        MatrixD expectedOutputs;
        MatrixD nextGradients;
        MatrixD expectedGradients;
        Pooler pooler;
        PoolingLayer layer;

        [TestInitializeAttribute]
        public void setup()
        {
            this.countEntries = GlobalRandom.NextInt(2,5);
            this.inDims = GlobalRandom.NextIntArr(countEntries, 2,5);
            this.entrySize = inDims.product();
            this.pooler = new Pooler(inDims.map(x=> GlobalRandom.NextInt(2, x)));
            this.layer = new PoolingLayer(this.pooler, this.inDims);

            MultiMatrix[] entries = ArrayBuilder.repeat(()=> MultiMatrix.Build.random(inDims), countEntries);
            MultiMatrix[] expectedOutputs = new MultiMatrix[countEntries];
            MultiMatrix[] nextGradients = new MultiMatrix[countEntries];
            MultiMatrix[] expectedGradients = new MultiMatrix[countEntries];
            for (int i = 0; i < countEntries; i++)
            {
                expectedOutputs[i] = pooler.slideOver(entries[i]);
                nextGradients[i] = MultiMatrix.Build.random(pooler.getOutputDims(inDims));
                expectedGradients[i] = pooler.getGradientInput(entries[i], nextGradients[i]);
            }

            this.inputs = entries.toMatrixD();
            this.expectedOutputs = expectedOutputs.toMatrixD();
            this.nextGradients = nextGradients.toMatrixD();
            this.expectedGradients = expectedGradients.toMatrixD();
        }

        [TestMethod]
        public void forward()
        {
            var outputs = layer.forward(inputs);
            Assert.IsTrue(outputs.EEquals(expectedOutputs));
        }

        [TestMethod]
        public void backward()
        {
            var gradients = layer.backward(inputs, nextGradients);
            Assert.IsTrue(gradients.EEquals(expectedGradients));
        }
    }
}