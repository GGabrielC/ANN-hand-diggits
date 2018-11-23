using ExtensionMethods;
using Global;
using Layers;
using MathNet.Numerics.LinearAlgebra;
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
        MatrixD annExpectedOutput;
        MatrixD[] layerInputs;
        Layer[] layers;
        
        double learnRate;
        LinkedList<double> costHistory;
        int maxSizeCostHistory;
        MatrixD annOutput;

        public Backprop(LayeredANN ann, MatrixD annExpectedOutput)
            => init(ann, annExpectedOutput);
        
        public void train(MatrixD annInput)
        {
            var iterations=10;
            var entries = 20;
            var randomEntriesIdx = GlobalRandom.NextIntArr(entries, 0, annInput.RowCount - 1);
            var currentInput = annInput.lines(randomEntriesIdx);
            var currendExpectedOutput = annExpectedOutput.lines(randomEntriesIdx);
            var costLayer = new CostLayer(currendExpectedOutput);

            forward(currentInput);
            updateFromNewCost(costLayer.forward(annOutput));
            for(var iteration = 1; iteration< iterations; iteration++)
            {
                checkPerformace(annOutput, currendExpectedOutput);
                Console.WriteLine("\nIteration: {0}", iteration);

                randomEntriesIdx = GlobalRandom.NextIntArr(entries, 0, annInput.RowCount - 1);
                currentInput = annInput.lines(randomEntriesIdx);
                currendExpectedOutput = annExpectedOutput.lines(randomEntriesIdx);
                costLayer = new CostLayer(currendExpectedOutput);

                backwardTrain(costLayer);
                forward(annInput.randomLines(entries));
                var annOut = annOutput.ToRowArrays();
                var outSum = annOutput.RowSums();
                updateFromNewCost(costLayer.forward(annOutput));
            }
            //Console.WriteLine("Setting trained layers!");
            ann.set(this.layers);
        }

        void forward(MatrixD inputs)
        {
            layerInputs[0] = inputs;
            for (int i=1; i<this.layers.Length; i++)
                layerInputs[i] = this.layers[i-1].forward(layerInputs[i-1]);
            this.annOutput = this.layers.Last().forward(layerInputs.Last());
        }

        void backwardTrain(CostLayer costLayer)
        {
            var gradients = costLayer.backward(annOutput);
            for (int i = layers.Length-1; i > 0; i--)
            {
                layers[i].backwardLearn(layerInputs[i], gradients, learnRate);
                gradients = layers[i].backward(layerInputs[i], gradients);
            }
            layers[0].backwardLearn(layerInputs[0], gradients, learnRate);
        }

        private void init(LayeredANN ann, MatrixD annExpectedOutput)
        {
            this.ann = ann;
            this.annExpectedOutput = annExpectedOutput;
            this.layers = ann.Layers.ToArray();
            this.layerInputs = new MatrixD[ann.LayersCount];

            this.learnRate = 0.05;
            this.costHistory = new LinkedList<double>(); ;
            this.maxSizeCostHistory = 10000;
        }

        private void updateFromNewCost(MatrixD newCost)
        {
            var cost = newCost.ColumnSums()[0];
            Console.WriteLine("Cost: {0}", cost);
            updateCostHistory(cost);
            updateRate();
        }

        private void updateRate()
        {
            if (true)
                learnRate *= 1;
            else learnRate *= 1;
        }

        private void updateCostHistory(double cost)
        {
            if (costHistory.Count() == maxSizeCostHistory)
                costHistory.RemoveFirst();
            costHistory.AddLast(cost);
        }

        private void checkPerformace(MatrixD annOutput, MatrixD expectedOutput)
        {
            var countSuccess = 0;
            for(var i=0; i<annOutput.RowCount; i++)
            {
                var maxIdx = 0;
                var max = annOutput[i,0];
                for(var j=0; j<annOutput.ColumnCount; j++)
                    if(max < annOutput[i,j])
                    {
                        max = annOutput[i, j];
                        maxIdx = j;
                    }
                if (expectedOutput[i, maxIdx].EEquals(1))
                    countSuccess++;
            }
            Console.WriteLine("Performance: {0}/{1}", countSuccess, expectedOutput.RowCount);
        }
    }
}
