using System;
using ExtensionMethods;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiMatrix_;
using Utils;

namespace UT_MultiMatrix
{
    [TestClass]
    public class UT_MultiMatrix
    {
        MultiMatrix[] splittedExpected;
        MultiMatrix mergedExpected;
        double[] mergedDataExp;
        int[] dimensionsExp;
        
        [TestInitializeAttribute]
        public void setup()
        {
            this.splittedExpected = new MultiMatrix[]
            {
                new MultiMatrix(new int[]{2,2}, new double[]{1,2,3,4}),
                new MultiMatrix(new int[]{2,2}, new double[]{5,6,7,8}),
                new MultiMatrix(new int[]{2,2}, new double[]{9,10,11,12}),
            };
            this.mergedDataExp = new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            this.mergedExpected = new MultiMatrix(new int[] { 3, 2, 2 }, this.mergedDataExp);
            this.dimensionsExp = new int[] { 3, 2, 2 };
        }

        [TestMethod]
        public void findIndex()
        {
            int[] dimensions = new int[] { 3, 1, 2, 1 };
            var mm = new MultiMatrix(dimensions);
            var index = mm.findIndex(new int[] { 2, 0, 0 });
            var expectedIndex = 4;
            Assert.AreEqual(index, expectedIndex);

            dimensions = new int[] { 2, 2 };
            mm = new MultiMatrix(dimensions);
            index = mm.findIndex(new int[] { 1, 1 });
            expectedIndex = 3;
            Assert.AreEqual(index, expectedIndex);
        }

        [TestMethod]
        public void getMergedDimensions()
        {
            MultiMatrix mm = new MultiMatrix(this.mergedExpected);
            Assert.IsTrue(mm.Dimensions.EEquals(this.dimensionsExp));
        }

        [TestMethod]
        public void getMergedData()
        {
            var mm = new MultiMatrix(splittedExpected);
            Assert.IsTrue(mm.Data.EEquals(this.mergedDataExp));
        }

        [TestMethod]
        public void split()
        {
            var mm = this.mergedExpected.split();
            Assert.IsTrue(mm.Length==this.splittedExpected.Length);
            for (var i = 0; i < mm.Length; i++)
                Assert.IsTrue(mm[i].EEquals(this.splittedExpected[i]));
        }
    }
}
