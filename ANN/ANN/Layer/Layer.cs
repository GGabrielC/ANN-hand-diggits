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
    public class Layer : ANNLayer
    {
        public override int Size { get => this.w.ColumnCount; }
        public MatrixD W { get => this.w; }
        public Vector<Double> B { get => this.b; }
        //public FuncDD[] Activations{ get => this.activations; }

        private MatrixD w = null;
        private Vector<Double> b = null;
        private FuncDD[] activations = null;

        private MatrixD z;
        private MatrixD a;
        private Layer previousLayer;

        public Layer(int size, int previousLayerSize)
        {
            setWeights(size, previousLayerSize);
            setBiases(size);
            setActivationFunctions(size);
        }

        public override MatrixD feed(MatrixD input)
        {
            var output = (input * this.W).addEachLine(b);
            return output.applyEachLine(activations);
        }
        
        public override void feedForTrain(MatrixD input)
        {
            this.z = (input * this.w).addEachLine(b);
            this.a = this.z.applyEachLine(activations);
            // TODO
        }

        public override void backPropagate(MatrixD partialDerivate) // TODO
        {
            var derivate = a.Transpose() * partialDerivate.scalarMultiplication(z);
            previousLayer.backPropagate(derivate);
        }

        private void setWeights(int size, int prevLayerSize)
            => this.w = MatrixD.Build.Random(prevLayerSize, size);

        private void setBiases(int size)
            => this.b = Vector<Double>.Build.Random(size);

        private void setActivationFunctions(int size)
            => this.activations = Enumerable.Repeat<FuncDD>(Functions.ReLU, size).ToArray();
    }
}
