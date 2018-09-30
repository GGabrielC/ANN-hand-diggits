﻿using Layers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANN
{
    public interface ANNTrainManager
    {
        void visit(CostLayer costLayer);
        void visit(HiddenLayer costLayer);
    }
}