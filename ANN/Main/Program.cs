using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloneExtensions;
using ExtensionMethods;
using Layers;
using MathNet.Numerics.LinearAlgebra;
using MNIST.IO;
using Utils;
using MatrixD = MathNet.Numerics.LinearAlgebra.Matrix<System.Double>;

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

            f();
            Console.ReadLine();
        }
        
        public static void f()
        {

        }

        public static void checkIterCoords()
        {
            MultiMatrix m = new MultiMatrix(new int[3] { 3,3,3 });
            foreach (var c in m.AllCoords(new int[] {2,2,2}))
                c.print();
        }
        

    }
}
