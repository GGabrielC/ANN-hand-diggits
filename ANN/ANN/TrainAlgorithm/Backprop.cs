using Layers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatrixD = MathNet.Numerics.LinearAlgebra.Matrix<System.Double>;

namespace ANN
{
    public class Backprop : Trainer
    {
        LayeredANN ann;
        MatrixD annInput;
        MatrixD annExpectedOutput;
        
        MatrixD[] layerInputs;
        Layer[] layers;
        CostLayer costLayer;

        double learnRate;
        LinkedList<MatrixD> costHistory;
        int maxSizeCostHistory;
        int iterationsLeft;
        MatrixD annOutput;

        public Backprop(LayeredANN ann, MatrixD annInput, MatrixD annExpectedOutput)
            => init(ann, annInput, annExpectedOutput);
        
        public void train()
        {
            this.layers = ann.Layers.ToArray();

            forward();
            updateFromNewCost(costLayer.forward(annOutput));

            while (iterationsLeft-- > 0)
            {
                backwardTrain();
                forward();
                updateFromNewCost(costLayer.forward(annOutput));
            }
            ann.setLayers(this.layers);
        }

        void forward()
        {
            layerInputs[0] = annInput;
            for (int i=1; i<this.layers.Length; i++)
                layerInputs[i] = this.layers[i-1].forward(layerInputs[i-1]);
            this.annOutput = this.layers.Last().forward(layerInputs.Last());
        }

        void backwardTrain()
        {
            var gradients = costLayer.backward(layerInputs.Last());
            for (int i = layers.Length-1; i > 0; i--)
            {
                layers[i].backwardLearn(layerInputs[i], gradients, learnRate);
                gradients = layers[i].backward(layerInputs[i], gradients);
            }
            layers[0].backwardLearn(layerInputs[0], gradients, learnRate);
        }

        private void init(LayeredANN ann, MatrixD annInput, MatrixD annExpectedOutput)
        {
            this.ann = ann;
            this.annInput = annInput;
            this.annExpectedOutput = annExpectedOutput;

            this.layerInputs = new MatrixD[ann.LayersCount];
            this.costLayer = new CostLayer(this.annExpectedOutput);

            this.learnRate = 1;
            this.costHistory = new LinkedList<MatrixD>(); ;
            this.maxSizeCostHistory = 10000;
            this.iterationsLeft = 1;
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
