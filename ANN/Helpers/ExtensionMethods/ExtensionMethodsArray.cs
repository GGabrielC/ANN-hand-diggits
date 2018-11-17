using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using MatrixD = MathNet.Numerics.LinearAlgebra.Matrix<System.Double>;

namespace ExtensionMethods
{
    public static class ExtensionMethodsArray
    {
        public static T[] ShallowCopy<T>(this T[] arr)
        {
            var copy = new T[arr.Length];
            arr.CopyTo(copy, 0);
            return copy;
        }

        public static void applyFunc<T>(this T[] arr, Func<T> func)
        {
            for (int i = 0; i < arr.Length; i++)
                arr[i] = func();
        }

        public static void applyFunc<T>(this T[] arr, Func<T, T> func)
        {
            for (int i = 0; i < arr.Length; i++)
                arr[i] = func(arr[i]);
        }

        //public static IEnumerable<Tm> map<T,Tm>(this IEnumerable<T> elems, Func<T, Tm> func)
          //  => elems.Select( func );
        
        public static Tm[] map<Tm, T>(this T[] arr, Func<T, Tm> func)
        {
            Tm[] result = new Tm[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                result[i] = func(arr[i]);
            return result;
        }
        
        public static Tm[] mapWith<Tm, T,T2>(this T[] arr, T2[] arr2, Func<T, T2, Tm> func)
        {
            Tm[] result = new Tm[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                result[i] = func(arr[i],arr2[i]);
            return result;
        }

        public static void forEach<T>(this T[] arr, Action<T> f)
        {
            foreach (var e in arr)
                f(e);
        }

        public static void forEachWith<T,T2>(this T[] arr1, T2[] arr2, Action<T, T2> f)
        {
            for (int i = 0; i < arr1.Length; i++)
                f(arr1[i], arr2[i]);
        }

        /*
        public static T[] map<T>(this T[] arr, Func<T, T> func)
        {
            var result = arr.ShallowCopy();
            result.applyFunc(func);
            return result;
        }

        public static T[] mapWith<T>(this T[] arr1, T[] arr2, Func<T, T, T> f)
        {
            var mapped = arr1.ShallowCopy();
            mapped.changeWith(arr2, f);
            return mapped;
        }
        */
        public static Boolean AllIndex<T>(this T[] arr1, Func<T,int,bool> func)
        {
            for (int i = 0; i < arr1.Length; i++)
                if ( false == func(arr1[i], i))
                    return false;
            return true;
        }

        public static void changeWith<T>(this T[] arr1, T[] arr2, Func<T,T,T> f)
        {
            for (int i = 0; i < arr1.Length; i++)
                arr1[i] = f(arr1[i], arr2[i]);
        }

        public static void addIn(this int[] arr1, int[] arr2)
        {
            for (int i = 0; i < arr2.Length; i++)
                arr1[i] += arr2[i];
        }

        public static int[] add(this int[] arr1, int[] arr2)
        {
            if(arr1.Length < arr2.Length)
            {
                var aux = arr1;
                arr1 = arr2;
                arr2 = aux;
            }
            var sum = arr1.ShallowCopy();
            sum.addIn(arr2);
            return sum;
        }

        public static void addIn(this double[] arr1, double[] arr2)
        {
            for (int i = 0; i < arr2.Length; i++)
                arr1[i] += arr2[i];
        }

        public static double[] add(this double[] arr1, double[] arr2)
        {
            if (arr1.Length < arr2.Length)
            {
                var aux = arr1;
                arr1 = arr2;
                arr2 = aux;
            }
            var sum = arr1.ShallowCopy();
            sum.addIn(arr2);
            return sum;
        }

        public static void addIn(this int[] arr, int num)
        {
            for (int i = 0; i < arr.Length; i++)
                arr[i] += num;
        }

        public static int[] add(this int[] arr, int num)
        {
            var sum = arr.ShallowCopy();
            sum.addIn(num);
            return sum;
        }
        
        public static int product(this int[] arr)
        {
            var result = 1;
            for (int i = 0; i < arr.Length; i++)
                if (arr[i] != 0)
                    result *= arr[i];
                else return 0;
            return result;
        }

        public static void multiplyIn(this double[] arr1, double[] arr2)
        {
            for (int i = 0; i < arr2.Length; i++)
                arr1[i] *= arr2[i];
        }

        public static double[] scalarMultiply(this double[] arr1, double[] arr2)
        {
            var arr = arr1.ShallowCopy();
            arr.multiplyIn(arr2);
            return arr;
        }

        public static void scalarMultiplyIn(this double[] arr, double scalar)
        {
            for (int i = 0; i < arr.Length; i++)
                arr[i] *= scalar;
        }

        public static double[] scalarMultiply(this double[] arr1, double scalar)
        {
            var arr = arr1.ShallowCopy();
            arr.scalarMultiplyIn(scalar);
            return arr;
        }

        public static MatrixD toMatrixD(this MultiMatrix[] data)
        {
            int countEntries = data.Length;
            var dataArr = new double[countEntries][];
            for (var i = 0; i < countEntries; i++)
                dataArr[i] = data[i].Data;
            return MatrixD.Build.DenseOfRowArrays(dataArr);
        }

        public static bool EEquals(this double[] m1, double[] m2, double epsilon = 0.000001)
        {
            if (m1.Length != m2.Length)
                return false;
            for (var i = 0; i < m1.Length; i++)
                if (!m1[i].EEquals(m2[i], epsilon))
                    return false;
            return true;
        }

        public static bool EEquals(this int[] m1, int[] m2)
        {
            if (m1.Length != m2.Length)
                return false;
            for (var i = 0; i < m1.Length; i++)
                if (m1[i] != m2[i])
                    return false;
            return true;
        }

        public static T[] flatten<T>(this T[][]arr)
        {
            int capacity = 0;
            for (int i = 0; i < arr.Length; i++)
                capacity += arr[i].Length;
            var elements = 0;
            var flat = new T[capacity];
            foreach (var a in arr)
            {
                a.CopyTo(flat, elements);
                elements += a.Length;
            }
            return flat;
        }

        public static void print<T>(this T[] arr)
        {
            Console.Write("[ ");
            for (int i = 0; i < arr.Length; i++)
                Console.Write(arr[i].ToString() + ", ");
            Console.WriteLine("]");
        }

        public static void printL<T>(this T[] arr)
        {
            Console.Write("[\n");
            for (int i = 0; i < arr.Length; i++)
                Console.Write(arr[i].ToString() + ",\n");
            Console.WriteLine("]");
        }
    }
}
