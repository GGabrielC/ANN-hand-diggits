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
            MultiMatrix[] mms = new MultiMatrix[]
            {
                new MultiMatrix(new int[]{2,2}, new double[]{1,2,3,4}),
                new MultiMatrix(new int[]{2,2}, new double[]{5,6,7,8}),
                new MultiMatrix(new int[]{2,2}, new double[]{9,10,11,12}),
            };
            MultiMatrix mm = new MultiMatrix(mms);
            Assert.IsTrue(mm.Dimensions.EEquals(new int[] { 3, 2, 2 }));
        }

        [TestMethod]
        public void getMergedData()
        {
            MultiMatrix[] mms = new MultiMatrix[]
            {
                new MultiMatrix(new int[]{2,2}, new double[]{1,2,3,4}),
                new MultiMatrix(new int[]{2,2}, new double[]{5,6,7,8}),
                new MultiMatrix(new int[]{2,2}, new double[]{9,10,11,12}),
            };
            MultiMatrix mm = new MultiMatrix(mms);
            Assert.IsTrue(mm.Data.EEquals(new double[] { 1,2,3,4,5,6,7,8,9,10,11,12 }));
        }
    }
}
