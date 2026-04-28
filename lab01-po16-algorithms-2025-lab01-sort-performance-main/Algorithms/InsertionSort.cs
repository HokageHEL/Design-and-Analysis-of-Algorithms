namespace Lab01SortPerformance.Algorithms;

public class InsertionSort : ISortingAlgorithm
{
    public string Name => "Insertion Sort";

    public SortingResult Sort(int[] array)
    {
        var arr = (int[])array.Clone();
        int comparisons = 0;
        int swaps = 0;

        for (int i = 1; i < arr.Length; i++)
        {
            int key = arr[i];
            int j = i - 1;

            while (j >= 0 && ++comparisons > 0 && arr[j] > key)
            {
                arr[j + 1] = arr[j];
                swaps++;
                j--;
            }
            arr[j + 1] = key;
        }

        return new SortingResult { Array = arr, Comparisons = comparisons, Swaps = swaps };
    }
}