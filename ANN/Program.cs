using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MNIST.IO;

namespace ANN
{
    class Program
    {

        static void Main(string[] args)
        {
            String labelsPath = "C:/Users/Gabriel/source/repos/ConsoleApp1/ANN/t10k-labels-idx1-ubyte.gz";
            String imagesPath = "C:/Users/Gabriel/source/repos/ConsoleApp1/ANN/t10k-images-idx3-ubyte.gz";
            var data = FileReaderMNIST.LoadImagesAndLables(labelsPath, imagesPath);
            var ann = new ANN(data);
            ann.train();

            Console.ReadLine();
        }
    }
}
