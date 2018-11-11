using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalItems
{
    public sealed class GlobalRandom
    {
        private static readonly Random instance = new Random();
        private GlobalRandom() { }
        public static Random Instance { get => instance; }
    }
}
