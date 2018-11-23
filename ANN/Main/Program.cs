using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ANN;
using CloneExtensions;
using ExtensionMethods;
using Global;
using Layers;
using MathNet.Numerics.LinearAlgebra;
using MNIST.IO;
using MNIST_SOLVER;
using Utils;
using MatrixD = MathNet.Numerics.LinearAlgebra.Matrix<System.Double>;

namespace MAIN
{
    class Program
    {
        static String imagesPath = "C:\\Users\\Gabriel\\source\\repos\\ConsoleApp1\\ANN\\MNIST_solver\\MNIST Input Files\\" +
            "t10k-images-idx3-ubyte.gz";
        static String labelsPath = "C:\\Users\\Gabriel\\source\\repos\\ConsoleApp1\\ANN\\MNIST_solver\\MNIST Input Files\\" +
            "t10k-labels-idx1-ubyte.gz";

        static void Main(string[] args)
        {
            f();
            Console.WriteLine("Done!");
            Console.ReadLine();
        }
        
        public static void f()
        {
            var ann = new NetworkBuilder().build();
            var labels = FileReaderMNIST.LoadLabel(labelsPath);
            var images = FileReaderMNIST.LoadImages(imagesPath);
            ann.train(images, labels);
        }

        public static void checkIterCoords()
        {
            MultiMatrix m = new MultiMatrix(new int[3] { 3,3,3 });
            foreach (var c in m.AllCoords(new int[] {2,2,2}))
                c.print();
        }
    }
}
