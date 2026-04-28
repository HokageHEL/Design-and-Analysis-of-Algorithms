namespace Lab01SortPerformance.Algorithms;

public class QuickSort : ISortingAlgorithm
{
    public string Name => "Quick Sort";

    public SortingResult Sort(int[] array)
    {
        var arr = (int[])array.Clone();
        int comparisons = 0;
        int swaps = 0;

        QuickSortRecursive(arr, 0, arr.Length - 1, ref comparisons, ref swaps);

        return new SortingResult { Array = arr, Comparisons = comparisons, Swaps = swaps };
    }

    private void QuickSortRecursive(int[] arr, int low, int high, ref int comparisons, ref int swaps)
    {
        if (low < high)
        {
            int pivotIndex = Partition(arr, low, high, ref comparisons, ref swaps);
            QuickSortRecursive(arr, low, pivotIndex - 1, ref comparisons, ref swaps);
            QuickSortRecursive(arr, pivotIndex + 1, high, ref comparisons, ref swaps);
        }
    }

    private int Partition(int[] arr, int low, int high, ref int comparisons, ref int swaps)
    {
        int pivot = arr[high];
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            comparisons++;
            if (arr[j] <= pivot)
            {
                i++;
                (arr[i], arr[j]) = (arr[j], arr[i]);
                swaps++;
            }
        }

        (arr[i + 1], arr[high]) = (arr[high], arr[i + 1]);
        swaps++;
        return i + 1;
    }
}