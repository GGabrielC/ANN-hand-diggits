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
    public class NormalizationLayer: Layer, ConvNetLayer
    {
        public int InSize => inSize;
        public int OutSize => inSize;
        public int[] InDims => inDims.ShallowCopy();
        public int[] OutDims => inDims.ShallowCopy();

        public FuncDD ActivationFunc { get => activationFunc; set => activationFunc = value; }
        public FuncDD DerivateActivationFunc => derivateActivationFunc;

        public int[] inDims;
        int inSize;

        FuncDD activationFunc;
        FuncDD derivateActivationFunc;

        public NormalizationLayer(int[] inDims)
            => set(Functions.ReLU, inDims);

        public NormalizationLayer(FuncDD activationFunc, int[] inDims)
            => set(activationFunc, inDims);

        public MatrixD forward(MatrixD inputData)
            => inputData.map(this.activationFunc);

        public MatrixD backward(MatrixD inputs, MatrixD gradient)
            => inputs.map(DerivateActivationFunc).scalarMultiply(gradient);

        public void backwardLearn(MatrixD inputs, MatrixD gradient, double learnRate)
        { }

        private void set(FuncDD activationFunc, int[] inDims)
        {
            this.inSize = inDims.product();
            this.inDims = inDims.ShallowCopy();
            this.activationFunc = activationFunc;
            this.derivateActivationFunc = Functions.getDerivate(activationFunc);
        }
    }
}
