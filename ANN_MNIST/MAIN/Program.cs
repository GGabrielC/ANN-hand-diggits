using ANN;
using GlobalRandom_;
using MNIST_SOLVER;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utils;
using XOR_;
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

            //new XORsolver().start();
        }
    }
}
