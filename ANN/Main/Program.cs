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

            int lines = 4;
            int cols = 4;
            MatrixD m1 = MatrixD.Build.Random(lines, cols);
            MatrixD m2 = MatrixD.Build.Random(lines, cols);
            MatrixD m3 = m1.Transpose() * m2;
            MatrixD m4 = m1.scalarMultiply(m2);
            m1.print();
            m2.print();
            m3.print();
            m4.print();
            m3.EEquals(m4);

            //checkIterCoords();
            Console.ReadLine();
        }
        
        public static void printArray(int[] arr)
        {
            Console.Write("[");
            for (int i = 0; i < arr.Length; i++)
                Console.Write(arr[i]+",");
            Console.WriteLine("]");
        }

        public static void checkIterCoords()
        {
            MultiMatrix m = new MultiMatrix(new int[3] { 3,3,3 });
            foreach (var c in m.AllCoords(new int[] {2,2,2}))
                printArray(c);
        }
        

    }
}
