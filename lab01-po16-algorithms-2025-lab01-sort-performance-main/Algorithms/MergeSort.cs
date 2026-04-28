namespace Lab01SortPerformance.Algorithms;

public class MergeSort : ISortingAlgorithm
{
    public string Name => "Merge Sort";

    public SortingResult Sort(int[] array)
    {
        var arr = (int[])array.Clone();
        int comparisons = 0;
        int swaps = 0;

        MergeSortRecursive(arr, 0, arr.Length - 1, ref comparisons, ref swaps);

        return new SortingResult { Array = arr, Comparisons = comparisons, Swaps = swaps };
    }

    private void MergeSortRecursive(int[] arr, int left, int right, ref int comparisons, ref int swaps)
    {
        if (left < right)
        {
            int mid = (left + right) / 2;
            MergeSortRecursive(arr, left, mid, ref comparisons, ref swaps);
            MergeSortRecursive(arr, mid + 1, right, ref comparisons, ref swaps);
            Merge(arr, left, mid, right, ref comparisons, ref swaps);
        }
    }

    private void Merge(int[] arr, int left, int mid, int right, ref int comparisons, ref int swaps)
    {
        int n1 = mid - left + 1;
        int n2 = right - mid;

        var L = new int[n1];
        var R = new int[n2];

        System.Array.Copy(arr, left, L, 0, n1);
        System.Array.Copy(arr, mid + 1, R, 0, n2);

        int i = 0, j = 0, k = left;

        while (i < n1 && j < n2)
        {
            comparisons++;
            if (L[i] <= R[j])
                arr[k++] = L[i++];
            else
            {
                arr[k++] = R[j++];
                swaps++;
            }
        }

        while (i < n1) arr[k++] = L[i++];
        while (j < n2) arr[k++] = R[j++];
    }
}