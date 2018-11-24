using ANN;
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

namespace TrainAlgorithm
{
    public class Backprop : Trainer
    {
        LayeredANN ann;
        double learnRate;
        LinkedList<double> costHistory;
        int maxSizeCostHistory;

        public Backprop(LayeredANN ann)
            => init(ann);
        
        public void train(MatrixD annInputs, MatrixD annExpectedOutputs)
        {
            var iterations = 100;
            var iter = new Iteration(ann.Layers.ToArray() , annInputs, annExpectedOutputs);
            
            for (var iteration = 1; iteration< iterations; iteration++)
            {
                Console.WriteLine("\nIteration: {0}", iteration);
                iter.next(learnRate);
                updateFromNewCost(iter.cost);
                checkPerformace(iter.currentAnnOutput, iter.currentExpectedOutput);

                /* * /
                var mO = iter.currentAnnOutput.ToRowArrays();
                var mEO = iter.currentExpectedOutput.ToRowArrays();
                var mOidx = iter.currentAnnOutput.maxIdxEachRow();
                var mEOidx = iter.currentExpectedOutput.maxIdxEachRow();
                //*/
            }
            //Console.WriteLine("Setting trained layers!");
            ann.set(iter.layers);
        }
        
        private void init(LayeredANN ann)
        {
            this.ann = ann;
            this.learnRate = 0.01;
            this.costHistory = new LinkedList<double>();
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
            var maxOutsIdx = annOutput.maxIdxEachRow();
            var maxEOutsIdx = expectedOutput.maxIdxEachRow();
            var outPairs = maxOutsIdx.Zip(maxEOutsIdx, (i1, i2) => new int[] {i1,i2 });
            var countSuccess = outPairs.Count(x=>x[0]==x[1]);
            
            var confusionMatrix = new int[expectedOutput.ColumnCount, expectedOutput.ColumnCount];
            foreach (var pair in outPairs)
                confusionMatrix[pair[0], pair[1]] += 1;

            Console.WriteLine("Performance: {0}/{1}", countSuccess, expectedOutput.RowCount);
            Console.WriteLine("Confusion Matrix:");
            confusionMatrix.printL();
        }
    }
}
