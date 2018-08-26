using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MNIST.IO;

namespace ANN
{
    class Program
    {

        static void Main(string[] args)
        {
            String labelsPath = "C:/Users/Gabriel/source/repos/ConsoleApp1/ANN/t10k-labels-idx1-ubyte.gz";
            String imagesPath = "C:/Users/Gabriel/source/repos/ConsoleApp1/ANN/t10k-images-idx3-ubyte.gz";

            //var ann = new ANN_MNIST();
            //ann.train(labelsPath, imagesPath);

            var arr = new double[3,3];
            for (var i = 0; i < 3; i++)
                for (var j = 0; j < 3; j++)
                    arr[i,j] = (j + 1) * (i + 1);
            var a = Matrix<double>.Build.SparseOfArray(arr);
            a = a.PointwisePower(2);
            Console.WriteLine(a.ToMatrixString());
            Console.WriteLine(a.ColumnSums().ToString());
            Console.ReadLine();
        }
    }
}
