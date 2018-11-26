using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethods
{
    public static class StringExtensions
    {
        public static String format(this String str, params object[] args)
            => String.Format(str, args);
    }
}