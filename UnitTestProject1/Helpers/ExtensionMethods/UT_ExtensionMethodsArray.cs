using System;
using ExtensionMethods;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UT_ExtensionMethods
{
    [TestClass]
    public class UT_ExtensionMethodsArray
    {
        [TestMethod]
        public void flatten()
        {
            var arrays = new int[4][];
            arrays[0] = new int[]{ 1,2,3};
            arrays[1] = new int[] { 4, 5 };
            arrays[2] = new int[] { 6, 7, 8 };
            arrays[3] = new int[] { 9};
            var expectedArr = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var arr = arrays.flatten();
            Assert.IsTrue(arr.EEquals(expectedArr));
        }
    }
}
