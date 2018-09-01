using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MNIST.IO;
using Utils;

namespace MAIN
{
    class Program
    {

        static void Main(string[] args)
        {
            String labelsPath = "C:/Users/Gabriel/source/repos/ConsoleApp1/ANN/MNIST/MNIST Input Files/" +
                "t10k-labels-idx1-ubyte.gz";
            String imagesPath = "C:/Users/Gabriel/source/repos/ConsoleApp1/ANN/MNIST/MNIST Input Files/" +
                "t10k-images-idx3-ubyte.gz";

            sandBox();
            
        }

        static public void sandBox()
        {

            String labelsPath = "C:/Users/Gabriel/source/repos/ConsoleApp1/ANN/MNIST/MNIST Input Files/" +
                "t10k-labels-idx1-ubyte.gz";
            String imagesPath = "C:/Users/Gabriel/source/repos/ConsoleApp1/ANN/MNIST/MNIST Input Files/" +
                "t10k-images-idx3-ubyte.gz";

            //var ann = new ANN_MNIST();
            //ann.train(labelsPath, imagesPath);

            var arr = new double[3, 3];
            for (var i = 0; i < 3; i++)
                for (var j = 0; j < 3; j++)
                    arr[i, j] = (j + 1) * (i + 1);
            var a = 2 * Matrix<double>.Build.DenseOfArray(arr);


            for (var i = 0; i < 3; i++)
                for (var j = 0; j < 3; j++)
                    arr[i, j] = (j + 1) * (i + 1);
            var b = Matrix<double>.Build.DenseOfArray(arr);

            var c = CostFunctions.squaredEuclidianDistance(a, b);

            Console.WriteLine(c.ToMatrixString());

            Console.ReadLine();
        }

    }
}
