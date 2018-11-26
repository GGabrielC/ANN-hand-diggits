using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using Layers;
using MatrixD = MathNet.Numerics.LinearAlgebra.Matrix<System.Double>;
using MathNet.Numerics.LinearAlgebra;
using ExtensionMethods;
using TrainAlgorithm;

namespace ANN
{
    public class Network: LayeredANN
    {
        public int InSize => layers.First().InSize;
        public int OutSize => layers.Last().OutSize;

        public IEnumerable<Layer> Layers => layers;// new List<Layer>(this.Layers);
        public int LayersCount { get => layers.Count(); }
        IEnumerable<Layer> layers;

        public Network() { }

        public Network(IEnumerable<Layer> layers)
            => set(layers);

        public MatrixD feedForward(MatrixD inputs)
        {
            var leyerOutput = inputs;
            foreach(var layer in layers)
                leyerOutput = layer.forward(leyerOutput);
            return leyerOutput;
        }

        public int classify(byte[,] image)
            => classify(MatrixD.Build.DenseOfRowArrays( new double[1][] { image.to1DimensionDoubles() }))[0];

        public int[] classify(MatrixD inputs)
            => feedForward(inputs).maxIdxEachRow();
        
        public void train(byte[][,] annInputs, byte[] annExpectedOutputs, 
                            int iterations = 100, int iterationSize = 50)
            => train(imagesToMatrixD(annInputs), labelsToMatrixD(annExpectedOutputs),
                                    iterations, iterationSize);

        public void train(MatrixD annInputs, MatrixD annExpectedOutputs, 
                            int iterations = 100, int iterationSize = 50)
            => new Backprop(this).train(annInputs, annExpectedOutputs, 
                                        iterations, iterationSize);
        
        public void set(IEnumerable<Layer> layers)
        {
            if (!validLayers(layers))
                throw new Exception("Topology is not Valid!");
            this.layers = layers;
        }

        public bool validLayers(IEnumerable<Layer> layers)
        {
            int prevLayerOutSize = layers.First().InSize;
            foreach (var l in layers)
                if (l.InSize != prevLayerOutSize)
                    return false;
                else prevLayerOutSize = l.OutSize;
            return true;
        }

        private MatrixD imagesToMatrixD(byte[][,] images)
        {
            var rawMatrix = new Double[images.Length, InSize];
            for (int i = 0; i < images.Length; i++)
                for (int j = 0; j < images[0].GetLength(0); j++)
                    for (int k = 0; k < images[k].GetLength(1); k++)
                        rawMatrix[i, j * images[0].GetLength(1) + k] = images[i][j, k];
            var x= MatrixD.Build.DenseOfArray(rawMatrix);
            return x;
        }

        private MatrixD labelsToMatrixD(byte[] labels)
        {
            var rawMatrix = new Double[labels.Count(), OutSize];
            for (int i = 0; i < labels.Length; i++)
                for (int j = 0; j < OutSize; j++)
                    if (j == labels[i])
                        rawMatrix[i, j] = 1;
                    else rawMatrix[i, j] = 0;
            return MatrixD.Build.DenseOfArray(rawMatrix);
        }
    }
}
