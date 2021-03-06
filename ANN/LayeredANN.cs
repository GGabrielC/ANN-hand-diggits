﻿using Layers;
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
        int InSize { get; }
        int OutSize { get; }
        int LayersCount { get; }
        IEnumerable<Layer> Layers { get; }
        void set(IEnumerable<Layer> layers);
        MatrixD feedForward(MatrixD inputs);
    }
}
