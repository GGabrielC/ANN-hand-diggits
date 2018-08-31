using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANN
{
    public class ActivationFunctions
    {
		public static Double ReLU(Double x)
		    => x < 0 ? 0 : x;
    }
}
