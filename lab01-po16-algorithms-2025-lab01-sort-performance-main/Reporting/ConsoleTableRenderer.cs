using System;
using System.Collections.Generic;
using Lab01SortPerformance.Models;

namespace Lab01SortPerformance.Reporting;

public class ConsoleTableRenderer : ITableRenderer
{
    private const string Top    = "┌─────────────────┬────────────┬────────────────┬──────────┬──────────────┬────────────┐";
    private const string Mid    = "├─────────────────┼────────────┼────────────────┼──────────┼──────────────┼────────────┤";
    private const string Bottom = "└─────────────────┴────────────┴────────────────┴──────────┴──────────────┴────────────┘";

    public void RenderPerformanceTable(List<ComplexityMeasurement> measurements)
    {
        Console.WriteLine();
        Console.WriteLine("╔═══════════════════════════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                          PERFORMANCE COMPARISON TABLE                             ║");
        Console.WriteLine("╚═══════════════════════════════════════════════════════════════════════════════════╝");

        Console.WriteLine(Top);
        Console.WriteLine($"│ {"Algorithm",-15} │ {"Array Size",10} │ {"Case",-14} │ {"Time(ms)",8} │ {"Comparisons",12} │ {"Swaps",10} │");
        Console.WriteLine(Mid);

        for (int m = 0; m < measurements.Count; m++)
        {
            var results = measurements[m].Results;

            for (int i = 0; i < results.Count; i++)
            {
                var r = results[i];
                string timeStr = r.ExecutionTimeMs < 0 ? "N/A" : $"{r.ExecutionTimeMs}";
                string correct = r.IsCorrect ? "" : " ❌";

                Console.WriteLine(
                    $"│ {r.AlgorithmName,-15} │ {r.ArraySize,10:N0} │ {r.TestCase,-14} │ {timeStr,8} │ {r.Comparisons,12:N0} │ {r.Swaps,10:N0} │{correct}");

                bool lastInMeasurement = (i == results.Count - 1);
                bool sizeChange = !lastInMeasurement && (i + 1) % 3 == 0;

                if (sizeChange)
                    Console.WriteLine(Mid);
            }

            if (m < measurements.Count - 1)
                Console.WriteLine(Mid);
            else
                Console.WriteLine(Bottom);
        }
    }
}