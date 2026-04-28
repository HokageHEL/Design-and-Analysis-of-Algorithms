namespace CityRoutePlanner;

public class DijkstraAlgorithm
{
    private Graph graph;
    private bool showSteps;

    public DijkstraAlgorithm(Graph graph, bool showSteps = false)
    {
        this.graph = graph;
        this.showSteps = showSteps;
    }

    public (Dictionary<City, int> distances, Dictionary<City, City?> previous) FindShortestPaths(City start)
    {
        var allCities = graph.GetAllCities();
        var distances = new Dictionary<City, int>();
        var previous = new Dictionary<City, City?>();
        var unvisited = new List<City>();

        foreach (var city in allCities)
        {
            distances[city] = int.MaxValue;
            previous[city] = null;
            unvisited.Add(city);
        }

        distances[start] = 0;

        if (showSteps)
            Console.WriteLine($"\n=== Starting Dijkstra's Algorithm from {start.Name} ===\n");

        while (unvisited.Count > 0)
        {
            var current = GetCityWithMinDistance(unvisited, distances);

            if (distances[current] == int.MaxValue)
                break;

            unvisited.Remove(current);

            if (showSteps)
                Console.WriteLine($"Visiting: {current.Name} (distance: {distances[current]} km)");

            foreach (var road in graph.GetRoadsFrom(current))
            {
                var neighbor = road.To;
                if (!unvisited.Contains(neighbor)) continue;

                int newDistance = distances[current] + road.Distance;

                if (showSteps)
                {
                    string currentDist = distances[neighbor] == int.MaxValue ? "∞" : $"{distances[neighbor]}";
                    Console.WriteLine($"  Checking neighbor: {neighbor.Name} (current distance: {currentDist} km, via {current.Name}: {newDistance} km)");
                }

                if (newDistance < distances[neighbor])
                {
                    distances[neighbor] = newDistance;
                    previous[neighbor] = current;

                    if (showSteps)
                        Console.WriteLine($"    ✓ Better path found to {neighbor.Name}: {newDistance} km");
                }
            }
        }

        return (distances, previous);
    }

    private City GetCityWithMinDistance(List<City> cities, Dictionary<City, int> distances)
    {
        City minCity = cities[0];
        int minDistance = distances[minCity];

        foreach (var city in cities)
        {
            if (distances[city] < minDistance)
            {
                minDistance = distances[city];
                minCity = city;
            }
        }

        return minCity;
    }

    public void DisplayShortestPath(City start, City end)
    {
        var (distances, previous) = FindShortestPaths(start);

        if (distances[end] == int.MaxValue)
        {
            Console.WriteLine($"\nNo path exists from {start.Name} to {end.Name}");
            return;
        }

        List<City> path = ReconstructPath(previous, end);

        Console.WriteLine($"\n=== Shortest Path from {start.Name} to {end.Name} ===");
        Console.WriteLine($"Total Distance: {distances[end]} km");
        Console.WriteLine($"\nRoute:");

        for (int i = 0; i < path.Count; i++)
        {
            if (i == 0)
            {
                Console.Write($"  {path[i].Name}");
            }
            else
            {
                Console.Write($" -> {path[i].Name}");
            }
        }
        Console.WriteLine("\n");
    }

    private List<City> ReconstructPath(Dictionary<City, City?> previous, City end)
    {
        List<City> path = new List<City>();
        City? current = end;

        while (current != null)
        {
            path.Insert(0, current);
            current = previous[current];
        }

        return path;
    }

    public List<City> GetShortestPath(City start, City end)
    {
        var (distances, previous) = FindShortestPaths(start);

        if (distances[end] == int.MaxValue)
        {
            return new List<City>();
        }

        return ReconstructPath(previous, end);
    }
}
