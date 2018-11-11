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

        public static void applyFunc<T>(this T[] arr, Func<T, T> func)
        {
            for (int i = 0; i < arr.Length; i++)
                arr[i] = func(arr[i]);
        }

        public static T[] map<T>(this T[] arr, Func<T, T> func)
        {
            var result = arr.ShallowCopy();
            result.applyFunc(func);
            return result;
        }

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

        public static T[] mapWith<T>(this T[] arr1, T[] arr2, Func<T, T, T> f)
        {
            var mapped = arr1.ShallowCopy();
            mapped.changeWith(arr2, f);
            return mapped;
        }

        public static void actionWith<T>(this T[] arr1, T[] arr2, Action<T, T> f)
        {
            for (int i = 0; i < arr1.Length; i++)
                f(arr1[i], arr2[i]);
        }

        public static void add(this int[] arr1, int[] arr2)
        {
            for (int i = 0; i < arr2.Length; i++)
                arr1[i] += arr2[i];
        }

        public static int[] addTo(this int[] arr1, int[] arr2)
        {
            if(arr1.Length < arr2.Length)
            {
                var aux = arr1;
                arr1 = arr2;
                arr2 = aux;
            }
            var sum = arr1.ShallowCopy();
            sum.add(arr2);
            return sum;
        }

        public static void addEach(this int[] arr, int num)
        {
            for (int i = 0; i < arr.Length; i++)
                arr[i] += num;
        }

        public static int[] addEachTo(this int[] arr, int num)
        {
            var sum = arr.ShallowCopy();
            sum.addEach(num);
            return sum;
        }
        
        public static int multiplyTo(this int[] arr)
        {
            var result = 1;
            for (int i = 0; i < arr.Length; i++)
                if (arr[i] != 0)
                    result *= arr[i];
                else return 0;
            return result;
        }

        public static void multiply(this double[] arr1, double[] arr2)
        {
            for (int i = 0; i < arr2.Length; i++)
                arr1[i] *= arr2[i];
        }

        public static MatrixD toMatrixD(this MultiMatrix[] data)
        {
            int countEntries = data.Length;
            var dataArr = new double[countEntries][];
            for (var i = 0; i < countEntries; i++)
                dataArr[i] = data[i].Data;
            return MatrixD.Build.DenseOfRowArrays(dataArr);
        }
    }
}
