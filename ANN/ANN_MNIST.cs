using MathNet.Numerics.LinearAlgebra;
using MNIST.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANN
{
    class ANN_MNIST : ANN
    {
        const int IMAGE_WIDTH = 28;
        const int IMAGE_HEIGHT = 28;
        const int COUNT_POSSIBLE_LABELS = 10;

        public ANN_MNIST() : base(IMAGE_WIDTH * IMAGE_HEIGHT, COUNT_POSSIBLE_LABELS) { }
        
        public void train(String labelsPath, String imagesPath)
        {
            var images = FileReaderMNIST.LoadImages(labelsPath);
            var labels = FileReaderMNIST.LoadLabel(labelsPath);
            this.train( asInput(images), asOutput(labels) );
        }
        
        public Matrix<Double> feedForward(String imagesPath)
        {
            var images = FileReaderMNIST.LoadImages(imagesPath);
            return this.feedForward( asInput(images) );
        }

        private Matrix<Double> asInput(IEnumerable<byte[,]> images)
        {
            var rawMatrix = new Double[images.Count(), this.InputSize];
            int i = 0;
            foreach (var image in images)
            {
                int j = 0;
                foreach (var pixel in image)
                    rawMatrix[i, j++] = pixel;
                i++;
            }
            return Matrix<Double>.Build.SparseOfArray(rawMatrix);
        }

        private Matrix<Double> asOutput(byte[] labels)
        {
            var a = FileReaderMNIST.LoadLabel("");
            var rawMatrix = new Double[labels.Count(), this.OutputSize];
            int i = 0;
            foreach (var label in labels)
                rawMatrix[i++, label] = 1;
            return Matrix<Double>.Build.SparseOfArray(rawMatrix);
        }

        private Matrix<Double> asInput(IEnumerable<MNIST.IO.TestCase> data)
        {
            var rawMatrix = new Double[data.Count(), this.InputSize];
            int i = 0;
            foreach (var testCase in data)
            {
                int j = 0;
                foreach (var pixel in testCase.Image)
                    rawMatrix[i, j++] = pixel;
                i++;
            }
            return Matrix<Double>.Build.SparseOfArray(rawMatrix);
        }
        
        private Matrix<Double> asOutput(IEnumerable<MNIST.IO.TestCase> data)
        {
            var rawMatrix = new Double[data.Count(), this.OutputSize];
            int i = 0;
            foreach (var testCase in data)
                rawMatrix[i++, testCase.Label] = 1;
            return Matrix<Double>.Build.SparseOfArray(rawMatrix);
        }
    }
}
