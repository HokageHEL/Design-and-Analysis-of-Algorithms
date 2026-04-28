namespace Lab01SortPerformance.Models;

public enum TestCase
{
    Sorted,
    Random,
    ReverseSorted,
}

public class SortingTestResult
{
    public string AlgorithmName { get; set; } = string.Empty;
    public int ArraySize { get; set; }
    public TestCase TestCase { get; set; } 
    public long ExecutionTimeMs { get; set; }
    public bool IsCorrect { get; set; }
    public int Comparisons { get; set; }
    public int Swaps { get; set; }

    public double ExecutionTimeSeconds => ExecutionTimeMs / 1000.0;
    
    public override string ToString()
    {
        return $"{AlgorithmName} | Size: {ArraySize} | Case: {TestCase} | Time: {ExecutionTimeMs}ms | Correct: {IsCorrect}";
    }
}