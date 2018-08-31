using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANN
{
    public class Layer: ANNLayer
    {
		public override int Size{ get => this.w.ColumnCount; }
		public Matrix<double> W{ get => this.w; }
		public Matrix<double> B{ get => this.b; }
		public Func<double, double>[] A{ get => this.a; }
        
        private Matrix<Double> w = null;
        private Matrix<Double> b = null;
        private Func<Double, Double>[] a = null;

        public Layer(int size, int previousLayerSize) // set when needed ?
		{
			setWeights(size, previousLayerSize);
			setBiases(size);
			setActivationFunctions(size);
		}

		public override Matrix<Double> feed(Matrix<double> input)
		{
			var output = input * W;
            // TODO
			return output;
		}

        public override Matrix<double> feedForTrain(Matrix<double> input)
        {
            throw new NotImplementedException();
        }

        private void setWeights(int size, int prevLayerSize) 
            => this.w = Matrix<Double>.Build.Random(prevLayerSize, size);

        private void setBiases(int size) 
            => this.b = Matrix<Double>.Build.Random(size, 1);

        private void setActivationFunctions(int size) 
            => this.a = Enumerable.Repeat<Func<Double, Double>>(ActivationFunctions.ReLU, size).ToArray()
    }
}
