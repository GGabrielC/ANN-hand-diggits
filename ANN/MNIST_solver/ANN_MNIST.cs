using MathNet.Numerics.LinearAlgebra;
using MNIST.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ANN;
using MatrixD = MathNet.Numerics.LinearAlgebra.Matrix<System.Double>;

namespace MNIST_SOLVER
{
    public class ANN_MNIST : ArtificalNN
    {
        public static readonly int IMAGE_WIDTH = 28;
        public static readonly int IMAGE_HEIGHT = 28;
        public static readonly int COUNT_POSSIBLE_LABELS = 10;
        public static readonly int ANN_INPUT_SIZE = IMAGE_WIDTH * IMAGE_HEIGHT;
        public static readonly int ANN_OUTPUT_SIZE = COUNT_POSSIBLE_LABELS;

        public ANN_MNIST() : base(ANN_INPUT_SIZE, ANN_OUTPUT_SIZE) { }

        public void train(String labelsPath, String imagesPath)
        {
            var images = FileReaderMNIST.LoadImages(labelsPath);
            var labels = FileReaderMNIST.LoadLabel(labelsPath);
            base.train( asInput(images), asOutput(labels) );
        }
        
        public MatrixD feed(String imagesPath)
        {
            var images = FileReaderMNIST.LoadImages(imagesPath);
            return base.feed( asInput(images) );
        }

        
        private MatrixD asInput(IEnumerable<byte[,]> images)
        {
            var rawMatrix = new Double[images.Count(), ANN_INPUT_SIZE];
            int i = 0;
            foreach (var image in images)
            {
                int j = 0;
                foreach (var pixel in image)
                    rawMatrix[i, j++] = pixel;
                i++;
            }
            return MatrixD.Build.DenseOfArray(rawMatrix);
        }

        private MatrixD asOutput(byte[] labels)
        {
            var rawMatrix = new Double[labels.Count(), ANN_OUTPUT_SIZE];
            int i = 0;
            foreach (var label in labels)
                rawMatrix[i++, label] = 1;
            return MatrixD.Build.DenseOfArray(rawMatrix);
        }

        private MatrixD AsInput(IEnumerable<MNIST.IO.TestCase> data)
        {
            var rawMatrix = new Double[data.Count(), ANN_INPUT_SIZE];
            int i = 0;
            foreach (var testCase in data)
            {
                int j = 0;
                foreach (var pixel in testCase.Image)
                    rawMatrix[i, j++] = pixel;
                i++;
            }
            return MatrixD.Build.DenseOfArray(rawMatrix);
        }

        private MatrixD asOutput(IEnumerable<MNIST.IO.TestCase> data)
        {
            var rawMatrix = new Double[data.Count(), ANN_OUTPUT_SIZE];
            int i = 0;
            foreach (var testCase in data)
                rawMatrix[i++, testCase.Label] = 1;
            return MatrixD.Build.DenseOfArray(rawMatrix);
        }
    }
}
