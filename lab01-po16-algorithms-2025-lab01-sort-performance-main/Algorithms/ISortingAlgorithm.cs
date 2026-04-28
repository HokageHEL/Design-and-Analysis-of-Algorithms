namespace Lab01SortPerformance.Algorithms;

public interface ISortingAlgorithm
{
    string Name { get; }
    
    SortingResult Sort(int[] array);
}