using GlobalRandom_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XOR_;
using FuncDD = System.Func<System.Double, System.Double>;
using MatrixD = MathNet.Numerics.LinearAlgebra.Matrix<System.Double>;

namespace _10x_y
{
    public class Solver10x
    {
        static int countIns = 5000000;
        static int batchSize = 100;
        static int iterations = 100;
        static FuncDD func = x => 11*x ;

        public Solver10x()
        { }
        
        public void start()
        {
            var inputs = new double[countIns][];
            var outputs = new double[countIns][];
            for (var i = 0; i < countIns; i++)
            {
                inputs[i] = new double[] { GlobalRandom.NextDouble(-100, 100) };
                outputs[i] = new double[] { func(inputs[i][0]) };
            }

            var ins = MatrixD.Build.DenseOfRowArrays(inputs);
            var outs = MatrixD.Build.DenseOfRowArrays(outputs);

            var nn = new net11Builder().build();
            nn.train(ins, outs, iterations, batchSize);
        }
    }
}
