using ANN;
using ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using MatrixD = MathNet.Numerics.LinearAlgebra.Matrix<System.Double>;
using FuncDD = System.Func<System.Double, System.Double>;

namespace Layers
{
    public class NormalizationLayer: Layer
    {
        public FuncDD ActivationFunc { get => activationFunc; set => activationFunc = value; }
        public FuncDD DerivateActivationFunc => derivateActivationFunc;
        public int InSize => inSize;
        public int OutSize => inSize;

        int inSize;
        FuncDD activationFunc;
        FuncDD derivateActivationFunc;

        public NormalizationLayer(int inSize)
        {
            this.inSize = inSize;
            this.activationFunc = Functions.ReLU;
            this.derivateActivationFunc = Functions.getDerivate(activationFunc);
        }
        public NormalizationLayer(FuncDD activationFunc, int inSize)
        {
            this.inSize = inSize;
            this.activationFunc = activationFunc;
        }
        
        public MatrixD forward(MatrixD inputData)
            => inputData.map(this.activationFunc);

        public MatrixD backward(MatrixD inputs, MatrixD gradient)
            => inputs.map(DerivateActivationFunc).scalarMultiply(gradient);

        public void backwardLearn(MatrixD inputs, MatrixD gradient, double learnRate)
        { }
    }
}
