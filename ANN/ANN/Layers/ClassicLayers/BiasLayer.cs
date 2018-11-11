using ANN;
using ExtensionMethods;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatrixD = MathNet.Numerics.LinearAlgebra.Matrix<System.Double>;

namespace Layers
{
    public class BiasLayer: Layer
    {
        public int OutSize { get => this.Biases.Count(); }
        public int InSize { get => this.Biases.Count(); }
        public double[] Biases { get => biases; }
        
        private double[] biases = null;

        public BiasLayer(int inputSize)
            => setBiases(inputSize);
        
        public MatrixD forward(MatrixD inputs)
            => inputs.addEachLine(this.Biases);

        public MatrixD backward(MatrixD inputs, MatrixD gradient)
            => gradient;

        public void backwardLearn(MatrixD inputs, MatrixD gradient, double learnRate)
            => biases.changeWith(gradient.ColumnSums().AsArray(), (b, g) => b - learnRate*g);

        private void setBiases( int inputSize)
            => this.biases = Vector<Double>.Build.Random(inputSize).AsArray();

        public MatrixD getDerivateToInput(MatrixD inputs)
            => MatrixD.Build.DenseOfRows(new double[1][] { Enumerable.Repeat(1.0, Biases.Count()).ToArray() });
        
    }
}
