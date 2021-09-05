using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranCook.Extensions
{
    /// <summary>
    /// Extension methods to use with ingredients grid
    /// Every 2D array is assumed to be square
    /// </summary>
    public static class ArrayExtensions
    {
        public static int[] RandomArray(int length, int floor, int ceil)
        {
            if (length <= 0) throw new ArgumentOutOfRangeException("length", "length should be above 0");

            int[] arr = new int[length];
            Random rnd = new Random();

            for(int i = 0; i < length; i++)
            {
                arr[i] = rnd.Next(floor, ceil);
            }

            if (arr.AllMatch()) 
                arr = RandomArray(length, floor, ceil);

            return arr;
        }

        public static void Populate(this int[] arr, int value)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = value;
            }
        }

        public static bool AllMatch(this int[] arr)
        {
            return Array.TrueForAll(arr, x => x == arr[0]);
        }

        public static void FillAll(this int[,] arr)
        {
            int len = arr.GetLength(0);

            for(int i = 0; i < len; i++)
            {
                // TODO set ceiling dynamically from different ingredients in recipe (and wildcard)
                arr.SetColumn(i, RandomArray(len, 0, 6));
            }

            VerifyRows(arr, len);
        }

        private static void VerifyColumns(int[,] arr, int len)
        {
            IList<int> fullMatchColIndices = new List<int>();
            for (int i = 0; i < len; i++)
            {
                int[] currentCol = arr.GetColumn(i);
                if (currentCol.AllMatch())
                {
                    fullMatchColIndices.Add(i);
                }
            }

            if (fullMatchColIndices.Count > 0)
            {
                foreach (int i in fullMatchColIndices)
                {
                    // TODO set ceiling dynamically from different ingredients in recipe (and wildcard)
                    arr.SetColumn(i, RandomArray(len, 0, 6));
                }
                VerifyRows(arr, len);
            }
        }

        private static void VerifyRows(int[,] arr, int len)
        {
            IList<int> fullMatchRowIndices = new List<int>();
            for(int i = 0; i < len; i++)
            {
                int[] currentRow = arr.GetRow(i);
                if(currentRow.AllMatch())
                {
                    fullMatchRowIndices.Add(i);
                }
            }

            if(fullMatchRowIndices.Count > 0)
            {
                foreach (int i in fullMatchRowIndices)
                {
                    // TODO set ceiling dynamically from different ingredients in recipe (and wildcard)
                    arr.SetRow(i, RandomArray(len, 0, 6));
                }
                VerifyColumns(arr, len);
            }
        }

        public static void FillNewCols(this int[,] arr, int colAmount)
        {
            int len = arr.GetLength(1);
            if (colAmount > len) throw new ArgumentOutOfRangeException("colAmount");

            for(int i = colAmount; i > 0; i--)
            {
                // TODO set ceiling dynamically from different ingredients in recipe (and wildcard)
                int[] newCol = RandomArray(len, 0, 6);
                arr.SetColumn(len - i, newCol);
            }
        }

        public static void FillNewRows(this int[,] arr, int rowAmount)
        {
            int len = arr.GetLength(0);
            if (rowAmount > len) throw new ArgumentOutOfRangeException("rowAmount");

            for (int i = 0; i < rowAmount; i++)
            {
                // TODO set ceiling dynamically from different ingredients in recipe (and wildcard)
                int[] newRow = RandomArray(len, 0, 6);
                arr.SetRow(i, newRow);
            }
        }

        public static int[] GetColumn(this int[,] arr, int colIndex)
        {
            return Enumerable.Range(0, arr.GetLength(0))
                .Select(x => arr[x, colIndex])
                .ToArray();
        }

        public static int[] GetRow(this int[,] arr, int rowIndex)
        {
            return Enumerable.Range(0, arr.GetLength(1))
                .Select(x => arr[rowIndex, x])
                .ToArray();
        }

        public static void SetColumn(this int[,] arr, int colIndex, int[] col)
        {
            int arrLen = arr.GetLength(0);
            int colLen = col.Length;
            if (colLen > arrLen) throw new ArgumentOutOfRangeException("col");
            if (colIndex > arrLen) throw new IndexOutOfRangeException();

            for (int i = 0; i < colLen; i++)
            {
                arr[i, colIndex] = col[i];
            }
        }

        public static void SetRow(this int[,] arr, int rowIndex, int[] row)
        {
            int arrLen = arr.GetLength(1);
            int rowLen = row.Length;
            if (rowLen > arrLen) throw new ArgumentOutOfRangeException("col");
            if (rowIndex > arrLen) throw new IndexOutOfRangeException();

            for(int i = 0; i < rowLen; i++)
            {
                arr[rowIndex, i] = row[i];
            }
        }

    }
}
