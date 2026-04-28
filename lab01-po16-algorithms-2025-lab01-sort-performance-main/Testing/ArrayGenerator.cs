using System;
using System.Linq;

namespace Lab01SortPerformance.Testing;

public static class ArrayGenerator
{
    private static readonly Random random = new Random(42); // Fixed seed for reproducible results
    
    public static int[] GenerateReverseSortedArray(int size)
    {
        return Enumerable.Range(1, size).Reverse().ToArray();
    }

    public static int[] GenerateSortedArray(int size)
    {
        return Enumerable.Range(1, size).ToArray();
    }

    public static int[] GenerateRandomlySortedArray(int size)
    {
        int[] arr = Enumerable.Range(1, size).ToArray();

        for (int i = arr.Length - 1; i >= 1; i--)
        {
            int j = random.Next(0, i + 1); // 0 to i inclusive
            (arr[i], arr[j]) = (arr[j], arr[i]); // tuple swap
        }

        return arr;
    }

    public static bool IsSorted(int[] array)
    {
        for (int i = 1; i < array.Length; i++)
        {
            if (array[i] < array[i - 1])
                return false;
        }
        return true;
    }
}