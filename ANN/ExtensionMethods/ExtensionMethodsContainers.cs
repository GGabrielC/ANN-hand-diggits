using CloneExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethods
{
    public static class ExtensionMethodsContainers
    {
        public static LinkedList<T> ShallowCopy<T>(this LinkedList<T> list)
        {
            var copy = new LinkedList<T>();
            foreach (var el in list)
                copy.AddLast(el.GetClone());
            return copy;
        }
    }
}
