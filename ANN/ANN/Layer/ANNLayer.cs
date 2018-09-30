using ANN;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatrixD = MathNet.Numerics.LinearAlgebra.Matrix<System.Double>;

namespace Layers
{
    public abstract class ANNLayer
    {
        public abstract int LayerInputSize { get; }
        public abstract int LayerOutputSize { get; }
        public ANNLayer NextLayer { get => nextLayer; set => nextLayer = value; }
        public bool IsLastLayer { get => NextLayer==null; }

        public abstract MatrixD feed(MatrixD layerInput);

        public MatrixD feedforward(MatrixD networkOutput)
        {
            var output = feed(networkOutput);
            if (NextLayer != null)
                return NextLayer.feedforward(output);
            return output;
        }
        
        public abstract void accept(ANNTrainManager visitor);

        private ANNLayer nextLayer = null;
    }
}
