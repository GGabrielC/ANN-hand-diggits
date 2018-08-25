using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANN
{
    public class Layer
    {
		int size;
		private Matrix<Double> w;
		private Matrix<Double> b = null;
		private Func<Double, Double>[] a = null;

		public int Size{ get => size; }
		public Matrix<double> W{ get => this.w; }
		public Matrix<double> B{ get => this.b; }
		public Func<double, double>[] A{ get => this.a; }

		public Layer(int size, Layer previousLayer)
		{
			this.size = size;
			setWeights(previousLayer.Size);
			setBiases();
			setActivationFunctions();
		}

		public Matrix<Double> feed(Matrix<double> input)
		{
			var output = input * W;
			return output;
		}

		void setWeights(int prevLayerSize) =>
			this.w = Matrix<Double>.Build.Random(prevLayerSize, this.Size);

		void setBiases() =>
			this.b = Matrix<Double>.Build.Random(this.Size, 1);

		void setActivationFunctions() =>
			this.a = Enumerable.Repeat<Func<Double, Double>>(ActivationFunctions.ReLU, Size).ToArray();
    }
}
