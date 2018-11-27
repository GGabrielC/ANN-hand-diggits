using ANN;
using GlobalRandom_;
using MNIST_SOLVER;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utils;
using MatrixD = MathNet.Numerics.LinearAlgebra.Matrix<System.Double>;

namespace WindowsFormsApp1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            /* */
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new App());
            //*/

            /* * /
            var nn = new XORnetBuilder().build();
            var xorTable = new double[][] {
                new double[] { 0, 0, 1 },
                new double[] { 1, 1, 1 },
                new double[] { 1, 0, 0 },
                new double[] { 0, 1, 0 },
            };
            var countIns = 100000;
            var indexes = GlobalRandom.NextIntArr(countIns, 0, xorTable.Length-1);
            var inputs = new double[countIns][];
            var outputs = new double[countIns][];
            for (var i = 0; i < countIns; i++)
            {
                inputs[i] = xorTable[indexes[i]].Take(2).ToArray();
                outputs[i] = xorTable[indexes[i]].Skip(2).ToArray();
            }

            var ins = MatrixD.Build.DenseOfRowArrays(inputs);
            var outs = MatrixD.Build.DenseOfRowArrays(outputs);
            var iterations = 10;
            nn.train(ins, outs, iterations, 10000);
            Console.Read();
            //*/
        }
    }
}
