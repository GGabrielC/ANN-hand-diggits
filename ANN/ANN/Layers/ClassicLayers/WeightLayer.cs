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
    public class WeightLayer: Layer
    {
        public int InSize { get => this.Weights.RowCount; }
        public int OutSize { get => this.Weights.ColumnCount; }
        public MatrixD Weights { get => weights; }
        
        private MatrixD weights = null;

        public WeightLayer(int inputSize, int outputSize)
            => setWeights(inputSize, outputSize);

        public WeightLayer(MatrixD weights)
            => this.weights = MatrixD.Build.DenseOfMatrix(weights);

        public MatrixD forward(MatrixD inputs)
            => inputs * this.Weights;

        public MatrixD backward(MatrixD inputs, MatrixD gradients)
        {
            var gradientsIn = new double[inputs.RowCount, inputs.ColumnCount];
            for(var i=0; i< inputs.RowCount; i++)
                for (var j = 0; j < InSize; j++)
                    for (var k = 0; k < OutSize; k++)
                        gradientsIn[i,j] += gradients.At(i,k)*weights.At(j,k);
            return MatrixD.Build.DenseOfArray(gradientsIn);
        }
        
        public void backwardLearn(MatrixD inputs, MatrixD gradients, double learnRate)
            => this.weights -= gradientWeights(inputs, gradients).scalarMultiply(learnRate); 

        public MatrixD gradientWeights(MatrixD inputs, MatrixD gradients)
        {
            var gradientW = new double[InSize, OutSize];
            for(var example=0; example<inputs.RowCount; example++)
                for (var i = 0; i < InSize; i++)
                    for (var j = 0; j < OutSize; j++)
                        gradientW[i,j] += gradients.At(example, j)*inputs.At(example, i);
            return MatrixD.Build.DenseOfArray(gradientW);
        }

        private void setWeights(int inputSize, int outputSize)
            => this.weights = MatrixD.Build.Random(inputSize, outputSize);
        
    }
}
