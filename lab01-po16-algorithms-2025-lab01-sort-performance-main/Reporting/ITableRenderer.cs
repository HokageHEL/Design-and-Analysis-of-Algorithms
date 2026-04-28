using System.Collections.Generic;
using Lab01SortPerformance.Models;

namespace Lab01SortPerformance.Reporting;

public interface ITableRenderer
{
    void RenderPerformanceTable(List<ComplexityMeasurement> measurements);
}