using Microsoft.VisualStudio.TestTools.UnitTesting;
using MNIST.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MNIST_SOLVER;

namespace UnitTestProject1
{
    [TestClass]
    class UT_ANN_MNIST
    {
        String labelsPath;
        String imagesPath;
        ANN_MNIST ann;

        [TestInitialize]
        public void testInit()
        {
            this.labelsPath = "C:/Users/Gabriel/source/repos/ConsoleApp1/ANN/MNIST/MNIST Input Files/" +
                "t10k-labels-idx1-ubyte.gz";
            this.imagesPath = "C:/Users/Gabriel/source/repos/ConsoleApp1/ANN/MNIST/MNIST Input Files/" +
                "t10k-images-idx3-ubyte.gz";
            this.ann = new ANN_MNIST();
        }

        [TestCleanup]
        public void testClean()
        {
            
        }

        [TestMethod]
        public void Test_asInput()
        {
            var images = FileReaderMNIST.LoadImages(labelsPath);
            var data = FileReaderMNIST.LoadImagesAndLables(labelsPath, labelsPath);
            
        }
        
        [TestMethod]
        public void Test_ANN_MNIST_InputSize()
            => Assert.Equals(ann.InputSize, 28*28);

        [TestMethod]
        public void Test_ANN_MNIST_OutputSize()
            => Assert.Equals(ann.OutputSize, 10);
        
    }
}
