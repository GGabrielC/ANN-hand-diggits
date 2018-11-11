using Layers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatrixD = MathNet.Numerics.LinearAlgebra.Matrix<System.Double>;

namespace ANN
{
    public interface LayeredANN
    {
        MatrixD feedForward(MatrixD inputs);

        int LayersCount { get; }
        IEnumerable<Layer> Layers { get; }
        void addLayer(Layer layer);
        void setLayers(Layer[] layers);
    }
}
