using GlobalRandom_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatrixD = MathNet.Numerics.LinearAlgebra.Matrix<System.Double>;


namespace XOR_
{
    public class XORsolver
    {
        static readonly double[][] xorTable;
        static int countIns = 1000000;
        static int iterSize = countIns;
        static int iterations = 100;

        public XORsolver()
        {}

        static XORsolver()
        {
            xorTable = new double[][] {
                new double[] { 0, 0, 1 },
                new double[] { 1, 1, 1 },
                new double[] { 1, 0, 0 },
                new double[] { 0, 1, 0 },
            };
        }

        public void start()
        {
            var inputs = new double[countIns][];
            var outputs = new double[countIns][];
            for (var i = 0; i < countIns; i++)
            {
                var rand = GlobalRandom.NextInt(0, xorTable.Length - 1);
                inputs[i] = xorTable[rand].Take(2).ToArray();
                outputs[i] = xorTable[rand].Skip(2).ToArray();
            }

            var ins = MatrixD.Build.DenseOfRowArrays(inputs);
            var outs = MatrixD.Build.DenseOfRowArrays(outputs);
            
            var nn = new XORnetBuilder().build();
            nn.train(ins, outs, iterations, iterSize);
        }
    }
}
