using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloneExtensions;
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
            Program.sandBox3();

            Console.ReadLine();
        }

        static public void sandBox3()
        {
            LinkedList<ANNLayer> list = new LinkedList<ANNLayer>();
            list.AddLast(new InputLayer(1));
            list.AddLast(new InputLayer(2));

            if(list.First.Value != l2.First.Value)
                Console.WriteLine("DA");
        }

        static public void sandBox2()
        {
            var a = EvenSequence(0, 10);
            var en = a.GetEnumerator();
            Console.WriteLine("1st: "+ en.Current);en.MoveNext();
            Console.WriteLine("2nd: "+ en.Current);en.MoveNext();
            Console.WriteLine("3rd: "+ en.Current);

            Console.ReadLine();
        }

        public static System.Collections.Generic.IEnumerable<int>
            EvenSequence(int firstNumber, int lastNumber)
        {
            yield return 1;
            yield return 2;
            yield return 3;
        }

        static public void sandBox1()
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
            var a = 2 * MatrixD.Build.DenseOfArray(arr);


            for (var i = 0; i < 3; i++)
                for (var j = 0; j < 3; j++)
                    arr[i, j] = (j + 1) * (i + 1);
            var b = MatrixD.Build.DenseOfArray(arr);

            //var c = CostFunctions.squaredEuclidianDistance(a, b);

            //Console.WriteLine(c.ToMatrixString());

            Console.ReadLine();
        }

    }
}
