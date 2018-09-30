using ExtensionMethods;
using Layers;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatrixD = MathNet.Numerics.LinearAlgebra.Matrix<System.Double>;
using SysColGen = System.Collections.Generic;

namespace ANN
{
    public class Backpropagation: ANNTrainManager
    {
        ArtificalNN ann;
        MatrixD annExpectedOutput;
        Dictionary<ANNLayer, MatrixD> derivatesForPreviousLayers;
        double learnRate;
        LinkedList<MatrixD> costHistory;
        int maxSizeCostHistory;
        MatrixD latestLayerOutput;

        MatrixD latestNetworkOutput;
        MatrixD LatestNetworkOutput
        {
            get => latestNetworkOutput;
            set
            {
                latestNetworkOutput = value;
            }
        }

        public Backpropagation(int maxSizeCostHistory = 50)
            => this.maxSizeCostHistory = maxSizeCostHistory;

        public void train(ArtificalNN ann, MatrixD annInput, MatrixD annExpectedOutput)
        {
            var trainedLayers = ann.LayersShallowCopy;
            trainedLayers.AddLast(new CostLayer(this.annExpectedOutput));
            for(int i=0; i<10000; i++)
                trainAllLayers(trainedLayers);
            trainedLayers.RemoveLast();
            ann.setLayers(trainedLayers);
        }

        public void visit(HiddenLayer layer)
        {
            var feedInfo = layer.feedForInfoDerivate(latestLayerOutput);
            latestLayerOutput = feedInfo.activity;
            layer.NextLayer.accept(this);

            var dCtoSum = derivatesForPreviousLayers[layer.NextLayer].scalarMultiplication(feedInfo.derivateAtoSum);

            var dCtoW = feedInfo.derivateSumToW.Transpose() * dCtoSum;
            layer.Weights -= learnRate * dCtoW;

            var dCtoB = feedInfo.derivateSumToB.Transpose() * dCtoSum;
            layer.Weights -= learnRate * dCtoB;

            var dCtoInput = feedInfo.derivateSumToInput.Transpose() * dCtoSum;
            derivatesForPreviousLayers[layer] = dCtoInput;
        }

        public void visit(CostLayer costLayer)
        {
            this.LatestNetworkOutput = this.latestLayerOutput;
            var cost = this.latestLayerOutput = costLayer.feed(this.latestLayerOutput);
            this.derivatesForPreviousLayers[costLayer] = costLayer.calculateDerivate(cost);
            this.updateFromNewCost(cost);
        }

        private void trainAllLayers(LinkedList<ANNLayer> trainLayers)
            => trainLayers.First.Value.accept(this);

        private void init(ArtificalNN ann, MatrixD annInput, MatrixD annExpectedOutput)
        {
            this.ann = ann;
            this.latestLayerOutput = annInput;
            this.annExpectedOutput = annExpectedOutput;
            this.derivatesForPreviousLayers = new Dictionary<ANNLayer, MatrixD>();
            this.LatestNetworkOutput = null;
            this.learnRate = 1;
            this.costHistory = new LinkedList<MatrixD>();
        }

        private void updateFromNewCost(MatrixD newCost)
        {
            updateCostHistory(newCost);
            updateRate();
        }

        private void updateRate()
        {
            if (true)
                learnRate *= 1;
            else learnRate *= 1;
        }

        private void updateCostHistory(MatrixD newCost)
        {
            if (costHistory.Count() == maxSizeCostHistory)
                costHistory.RemoveFirst();
            costHistory.AddLast(newCost);
        }
    }
}
