using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethods
{
    public static class ContainersExtensions
    {
        public static LinkedList<T> ShallowCopy<T>(this LinkedList<T> list)
        {
            var copy = new LinkedList<T>();
            foreach (var el in list)
                copy.AddLast(el);
            return copy;
        }

        public static IEnumerable<Tm> map<T,Tm>(this IEnumerable<T> elems, Func<T, Tm> func)
          => elems.Select( func );
    }
}
