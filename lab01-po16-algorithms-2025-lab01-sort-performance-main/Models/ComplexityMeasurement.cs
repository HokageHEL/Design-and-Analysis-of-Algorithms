using System.Collections.Generic;

namespace Lab01SortPerformance.Models;

public class ComplexityMeasurement
{
    public List<SortingTestResult> Results { get; set; } = new();
    
    public void AddResult(SortingTestResult result)
    {
        Results.Add(result);
    }
}