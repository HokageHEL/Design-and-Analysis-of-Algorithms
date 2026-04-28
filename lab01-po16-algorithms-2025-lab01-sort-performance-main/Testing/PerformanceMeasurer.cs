using System;
using System.Collections.Generic;
using System.Diagnostics;
using Lab01SortPerformance.Algorithms;
using Lab01SortPerformance.Models;

namespace Lab01SortPerformance.Testing;

public class PerformanceMeasurer
{
    private readonly int[] testSizes = { 1000, 5000, 10000, 25000 };
    private readonly int warmupRuns = 3;
    private readonly int measurementRuns = 5;


    public ComplexityMeasurement MeasureAlgorithm(ISortingAlgorithm algorithm)
    {
        var measurement = new ComplexityMeasurement();

        Console.WriteLine($"\n=== 📊 Measuring {algorithm.Name} ===");
        Console.WriteLine();

        foreach (var size in testSizes)
        {
            Console.WriteLine($"Testing array size: {size:N0}");

            var sortedResult = MeasureTestCase(algorithm, size, TestCase.Sorted, ArrayGenerator.GenerateSortedArray);
            var randomResult = MeasureTestCase(algorithm, size, TestCase.Random,
                ArrayGenerator.GenerateRandomlySortedArray);
            var reverseSortedResult = MeasureTestCase(algorithm, size, TestCase.ReverseSorted,
                ArrayGenerator.GenerateReverseSortedArray);

            measurement.AddResult(sortedResult);
            measurement.AddResult(randomResult);
            measurement.AddResult(reverseSortedResult);

            Console.WriteLine(
                $"  Sorted:        {sortedResult.ExecutionTimeMs,6}ms | Comparisons: {sortedResult.Comparisons,8:N0} | Swaps: {sortedResult.Swaps,8:N0}");
            Console.WriteLine(
                $"  Random:        {randomResult.ExecutionTimeMs,6}ms | Comparisons: {randomResult.Comparisons,8:N0} | Swaps: {randomResult.Swaps,8:N0}");
            Console.WriteLine(
                $"  ReverseSorted: {reverseSortedResult.ExecutionTimeMs,6}ms | Comparisons: {reverseSortedResult.Comparisons,8:N0} | Swaps: {reverseSortedResult.Swaps,8:N0}");
            Console.WriteLine();
        }

        return measurement;
    }

    private SortingTestResult MeasureTestCase(ISortingAlgorithm algorithm, int size, TestCase testCase,
        Func<int, int[]> arrayGenerator)
    {
        var testResult = new SortingTestResult
        {
            AlgorithmName = algorithm.Name,
            ArraySize = size,
            TestCase = testCase
        };

        var times = new List<long>();
        var allComparisons = new List<int>();
        var allSwaps = new List<int>();
        bool allCorrect = true;

        // Warmup runs
        for (int i = 0; i < warmupRuns; i++)
        {
            var warmupArray = arrayGenerator(size);
            var result = algorithm.Sort(warmupArray);
        }

        // Measurement runs
        for (int i = 0; i < measurementRuns; i++)
        {
            var testArray = arrayGenerator(size);

            var stopwatch = Stopwatch.StartNew();

            SortingResult result;
            try
            {
                result = algorithm.Sort(testArray);
            }
            catch (NotImplementedException)
            {
                testResult.IsCorrect = false;
                testResult.ExecutionTimeMs = -1;
                return testResult;
            }

            stopwatch.Stop();

            times.Add(stopwatch.ElapsedMilliseconds);
            allComparisons.Add(result.Comparisons);
            allSwaps.Add(result.Swaps);

            // Verify correctness
            if (ArrayGenerator.IsSorted(result.Array)) continue;
            allCorrect = false;
            Console.WriteLine($"  ERROR: Array not sorted correctly for {testCase} case!");
        }

        // Use median time to reduce impact of outliers
        times.Sort();
        allComparisons.Sort();
        allSwaps.Sort();

        testResult.ExecutionTimeMs = times[times.Count / 2];
        testResult.Comparisons = allComparisons[allComparisons.Count / 2];
        testResult.Swaps = allSwaps[allSwaps.Count / 2];
        testResult.IsCorrect = allCorrect;

        return testResult;
    }
}
