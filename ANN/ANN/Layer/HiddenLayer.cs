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
    public class HiddenLayer : ANNLayer
    {
        public override int LayerInputSize { get => this.Weights.RowCount; }
        public override int LayerOutputSize { get => this.layerSize; }
        public MatrixD Weights { get => weights; set => weights = value; }
        public Vector<double> Biases { get => biases; set => biases = value; }
        public List<FuncDD> Activations { get => activations; set => activations = value; }
        public MatrixD BiasDerivates
            => MatrixD.Build.DenseOfRows(new double[1][] { Enumerable.Repeat(1.0, this.Biases.Count()).ToArray() });

        private int layerSize;
        private List<FuncDD> activations = null;
        private MatrixD weights = null;
        private Vector<Double> biases = null;
        
        public HiddenLayer(int layerSize, int prevLayerSize, ANNLayer nextLayer=null)
        {
            this.layerSize = layerSize;
            setWeights(prevLayerSize);
            setBiases();
            setActivationFunctions();
            base.NextLayer = nextLayer;
        }

        public override MatrixD feed(MatrixD layerInput)
            => calculateActivity(feedForBiasedWeightedSum(layerInput));

        public MatrixD feedForBiasedWeightedSum(MatrixD layerInput)
            => calculateBiasedWeightedSum(feedForUnbiasedWeightedSum(layerInput));

        public MatrixD feedForUnbiasedWeightedSum(MatrixD layerInput)
            => (layerInput * this.Weights);

        public MatrixD calculateBiasedWeightedSum(MatrixD unbiasedWeightedSum)
            => unbiasedWeightedSum.addEachLine(this.Biases);

        public MatrixD calculateActivity(MatrixD biasedWeightedSum)
            => biasedWeightedSum.applyEachLine(this.Activations);
        
        public InfoFeedForDerivate feedForInfoDerivate(MatrixD layerInput)
        {
            InfoFeedForDerivate info = new InfoFeedForDerivate();
            info.biasedWeightedSum = feedForBiasedWeightedSum(layerInput);
            info.activity = calculateActivity(info.biasedWeightedSum);
            info.derivateAtoSum = info.biasedWeightedSum.applyEachLine(Functions.getDerivates(activations));
            info.derivateSumToW = layerInput;
            info.derivateSumToB = BiasDerivates;
            info.derivateSumToInput = Weights;
            return info;
        }
        
        public override void accept(ANNTrainManager visitor)
            => visitor.visit(this);
    
        private void setWeights(int prevLayerSize)
            => this.Weights = MatrixD.Build.Random(prevLayerSize, this.LayerOutputSize);

        private void setBiases()
            => this.Biases = Vector<Double>.Build.Random(this.layerSize);

        private void setActivationFunctions()
            => this.Activations = Enumerable.Repeat<FuncDD>(Functions.ReLU, this.layerSize).ToList();

        public struct InfoFeedForDerivate
        {
            internal MatrixD activity;
            internal MatrixD biasedWeightedSum;
            internal MatrixD derivateAtoSum;
            internal MatrixD derivateSumToW;
            internal MatrixD derivateSumToB;
            internal MatrixD derivateSumToInput;
        }
    }
}