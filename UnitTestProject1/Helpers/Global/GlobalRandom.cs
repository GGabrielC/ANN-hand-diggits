using System;
using System.Collections.Generic;
using Global;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utils;

namespace UT_Global
{
    [TestClass]
    public class UT_GlobalRandom
    {
        [TestMethod]
        public void NextInt()
        {
            int minVal = -2, maxVal = 2, count = 100;
            var nums = new List<int>(ArrayBuilder.repeat(()=>GlobalRandom.NextInt(minVal, maxVal), count));
            foreach (var num in nums)
                Assert.IsTrue(num >= -2 && num <= 2);
            Assert.IsTrue(nums.Contains(-2));
            Assert.IsTrue(nums.Contains(-1));
            Assert.IsTrue(nums.Contains( 0));
            Assert.IsTrue(nums.Contains( 1));
            Assert.IsTrue(nums.Contains( 2));
        }

        [TestMethod]
        public void NextIntArr()
        {
            int minVal = -2, maxVal = 2, count = 100;
            var nums = new List<int>(GlobalRandom.NextIntArr(count, minVal, maxVal));
            Assert.IsTrue(nums.Count == count);
            foreach (var num in nums)
                Assert.IsTrue(num >= -2 && num <= 2);
            Assert.IsTrue(nums.Contains(-2));
            Assert.IsTrue(nums.Contains(-1));
            Assert.IsTrue(nums.Contains(0));
            Assert.IsTrue(nums.Contains(1));
            Assert.IsTrue(nums.Contains(2));
        }

        [TestMethod]
        public void NextDouble()
        {
            // no params
            int count = 30, minVal = 0, maxVal = 1;
            var nums = new List<double>(ArrayBuilder.repeat(() => GlobalRandom.NextDouble(), count));
            foreach (var num in nums)
                Assert.IsTrue(num >= minVal && num < maxVal);
            
            // count
            nums = new List<double>(GlobalRandom.NextDoubleArr(count));
            Assert.IsTrue(nums.Count == count);
            foreach (var num in nums)
                Assert.IsTrue(num >= minVal && num < maxVal);

            // count, min, max
            minVal = -2; maxVal = 2;
            nums = new List<double>(GlobalRandom.NextDoubleArr(count, minVal, maxVal));
            Assert.IsTrue(nums.Count == count);
            foreach (var num in nums)
                Assert.IsTrue(num >= minVal && num < maxVal);
        }

    }
}
