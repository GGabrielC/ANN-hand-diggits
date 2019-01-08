using ANN;
using ExtensionMethods;
using GlobalRandom_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatrixD = MathNet.Numerics.LinearAlgebra.Matrix<System.Double>;

namespace TrainAlgorithm
{
    public class Iteration
    {
        public MatrixD[] layerInputs;
        public MatrixD preCost;
        public MatrixD postCost;
        public MatrixD currentAnnOutput;
        public MatrixD currentInput;
        public MatrixD currentExpectedOutput;
        public Layer[] layers;

        MatrixD annInputs;
        MatrixD annExpectedOutputs;

        public Iteration(Layer[] layers, MatrixD annInputs, MatrixD annExpectedOutputs)
        {
            this.annExpectedOutputs = annExpectedOutputs;
            this.annInputs = annInputs;
            this.layers = layers;
            this.layerInputs = new MatrixD[layers.Count()];
        }

        public void next(double learnRate, int countEntries)
        {
            preCost = postCost = null;
            var randomEntriesIdx = GlobalRandom.NextIntArr(countEntries, 0, annInputs.RowCount - 1);
            this.currentInput = annInputs .lines(randomEntriesIdx);
            this.currentExpectedOutput = annExpectedOutputs.lines(randomEntriesIdx);

            var costLayer = new CostLayer(currentExpectedOutput);
            forward(currentInput, costLayer);
            backwardTrain(costLayer, learnRate/countEntries);
            
            /* * /
            var mO = annOutput.ToRowArrays();
            var mEO = currentExpectedOutput.ToRowArrays();

            var mOidx = annOutput.maxIdxEachRow();
            var mEOidx = currentExpectedOutput.maxIdxEachRow();
            //*/
        }

        void forward(MatrixD inputs, CostLayer costLayer)
        {
            layerInputs[0] = inputs;
            for (int i = 1; i < this.layers.Length; i++)
                layerInputs[i] = this.layers[i - 1].forward(layerInputs[i - 1]);
            this.currentAnnOutput = this.layers.Last().forward(layerInputs.Last());
            if(preCost==null)
                preCost = costLayer.forward(this.currentAnnOutput);
            else postCost = costLayer.forward(this.currentAnnOutput);
            //var m = annOutput.ToRowArrays();
        }

        void backwardTrain(CostLayer costLayer, double learnRate)
        {
            var gradients = costLayer.backward(currentAnnOutput);
            for (int i = layers.Length - 1; i > 0; i--)
            {
                layers[i].backwardLearn(layerInputs[i], gradients, learnRate);
                gradients = layers[i].backward(layerInputs[i], gradients);
            }
            layers[0].backwardLearn(layerInputs[0], gradients, learnRate);
        }
    }
}
