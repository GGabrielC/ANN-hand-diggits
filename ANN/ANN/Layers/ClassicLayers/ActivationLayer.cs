using ANN;
using ExtensionMethods;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using FuncDD = System.Func<System.Double, System.Double>;
using MatrixD = MathNet.Numerics.LinearAlgebra.Matrix<System.Double>;

namespace Layers
{
    public class ActivationLayer: Layer
    {
        public int OutSize { get => this.Activations.Count(); }
        public int InSize { get => this.Activations.Count(); }
        public FuncDD[] Activations => activations;
        public FuncDD[] DerivateActivations => derivateActivations;

        private FuncDD[] activations = null;
        private FuncDD[] derivateActivations = null;

        public ActivationLayer(int inputSize)
            => set(inputSize, Enumerable.Repeat<FuncDD>(Functions.ReLU, inputSize).ToArray());

        public ActivationLayer(int inputSize, FuncDD[] activations)
            => set(inputSize, activations);

        public ActivationLayer(int inputSize, FuncDD activation)
            => set(inputSize, Enumerable.Repeat<FuncDD>(activation, inputSize).ToArray());

        public MatrixD forward(MatrixD inputs)
            => inputs.mapLines(this.Activations);

        public MatrixD backward(MatrixD inputs, MatrixD gradient)
            => inputs.mapLines(derivateActivations).scalarMultiply(gradient);
        
        public void backwardLearn(MatrixD inputs, MatrixD gradient, double learnRate)
        {}

        private void set(int inputSize, FuncDD[] activations)
        {
            this.activations = activations;
            this.derivateActivations = Functions.getDerivates(Activations);
        }
    }
}
