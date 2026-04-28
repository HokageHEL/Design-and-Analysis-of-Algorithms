namespace CityRoutePlanner;

public class Graph
{
    /// <summary>
    /// Dictionary of cities and roads to other cities
    /// </summary>
    private Dictionary<City, List<Road>> adjacencyList;

    public Graph()
    {
        adjacencyList = new Dictionary<City, List<Road>>();
    }

    public void AddCity(City city)
    {
        if (!adjacencyList.ContainsKey(city))
        {
            adjacencyList[city] = new List<Road>();
        }
    }

    public void AddRoad(City from, City to, int distance)
    {
        AddCity(from);
        AddCity(to);
        adjacencyList[from].Add(new Road(from, to, distance));
        adjacencyList[to].Add(new Road(to, from, distance));
    }

    public List<City> GetAllCities()
    {
        return new List<City>(adjacencyList.Keys);
    }

    public List<Road> GetRoadsFrom(City city)
    {
        if (!adjacencyList.ContainsKey(city))
            return new List<Road>();
        return adjacencyList[city];
    }

    public void DisplayAllConnections()
    {
        Console.WriteLine("\n=== City Network ===");
        foreach (var city in adjacencyList.Keys)
        {
            Console.WriteLine($"\n{city.Name}:");
            foreach (var road in adjacencyList[city])
            {
                Console.WriteLine($"  -> {road.To.Name} ({road.Distance} km)");
            }
        }
        Console.WriteLine();
    }
}
