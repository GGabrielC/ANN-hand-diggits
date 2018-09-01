using ExtensionMethods;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Layers
{
    public class Layer : ANNLayer
    {
        public override int Size { get => this.w.ColumnCount; }
        public Matrix<double> W { get => this.w; }
        public Vector<Double> B { get => this.b; }
        //public Func<double, double>[] Activations{ get => this.activations; }

        private Matrix<Double> w = null;
        private Vector<Double> b = null;
        private Func<Double, Double>[] activations = null;

        private Matrix<Double> z;
        private Matrix<Double> a;

        public Layer(int size, int previousLayerSize)
        {
            setWeights(size, previousLayerSize);
            setBiases(size);
            setActivationFunctions(size);
        }

        public override Matrix<Double> feed(Matrix<double> input)
        {
            var output = (input * this.W).addEachLine(b);
            return output.applyEachLine(activations);
        }

        public override void feedForTrain(Matrix<double> input)
        {
            this.z = (input * this.w).addEachLine(b);
            this.a = this.z.applyEachLine(activations);
            // TODO
        }

        private void setWeights(int size, int prevLayerSize)
            => this.w = Matrix<Double>.Build.Random(prevLayerSize, size);

        private void setBiases(int size)
            => this.b = Vector<Double>.Build.Random(size);

        private void setActivationFunctions(int size)
            => this.activations = Enumerable.Repeat<Func<Double, Double>>(ActivationFunctions.ReLU, size).ToArray();
    }
}
