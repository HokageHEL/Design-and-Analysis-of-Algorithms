using CityRoutePlanner;

class Program
{
    static void Main(string[] args)
    {
        Graph graph = CreateCityNetwork();

        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║   City Route Planner                   ║");
        Console.WriteLine("║   Dijkstra's Shortest Path Algorithm   ║");
        Console.WriteLine("╚════════════════════════════════════════╝");

        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. View all cities and connections");
            Console.WriteLine("2. Find shortest path (without steps)");
            Console.WriteLine("3. Find shortest path (with algorithm steps)");
            Console.WriteLine("4. Visualize graph");
            Console.WriteLine("5. Find shortest path and visualize");
            Console.WriteLine("6. Exit");
            Console.Write("\nChoose an option: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    graph.DisplayAllConnections();
                    break;
                case "2":
                    FindPath(graph, showSteps: false);
                    break;
                case "3":
                    FindPath(graph, showSteps: true);
                    break;
                case "4":
                    VisualizeGraph(graph);
                    break;
                case "5":
                    FindPathAndVisualize(graph);
                    break;
                case "6":
                    Console.WriteLine("\nGoodbye!");
                    return;
                default:
                    Console.WriteLine("\nInvalid choice. Please try again.");
                    break;
            }
        }
    }

    static Graph CreateCityNetwork()
    {
        Graph graph = new Graph();

        City kyiv = new City("Kyiv");
        City lviv = new City("Lviv");
        City odesa = new City("Odesa");
        City kharkiv = new City("Kharkiv");
        City dnipro = new City("Dnipro");
        City zaporizhzhia = new City("Zaporizhzhia");
        City vinnytsia = new City("Vinnytsia");
        City chernihiv = new City("Chernihiv");

        graph.AddRoad(kyiv, lviv, 540);
        graph.AddRoad(kyiv, odesa, 475);
        graph.AddRoad(kyiv, kharkiv, 480);
        graph.AddRoad(kyiv, dnipro, 480);
        graph.AddRoad(kyiv, vinnytsia, 270);
        graph.AddRoad(kyiv, chernihiv, 145);

        graph.AddRoad(lviv, vinnytsia, 360);
        graph.AddRoad(lviv, odesa, 790);

        graph.AddRoad(odesa, vinnytsia, 430);
        graph.AddRoad(odesa, dnipro, 550);

        graph.AddRoad(kharkiv, dnipro, 215);
        graph.AddRoad(kharkiv, chernihiv, 330);

        graph.AddRoad(dnipro, zaporizhzhia, 85);
        graph.AddRoad(dnipro, vinnytsia, 510);

        graph.AddRoad(zaporizhzhia, odesa, 480);

        return graph;
    }

    static void FindPath(Graph graph, bool showSteps)
    {
        List<City> cities = graph.GetAllCities();

        Console.WriteLine("\nAvailable cities:");
        for (int i = 0; i < cities.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {cities[i].Name}");
        }

        Console.Write("\nEnter start city number: ");
        if (!int.TryParse(Console.ReadLine(), out int startIndex) ||
            startIndex < 1 || startIndex > cities.Count)
        {
            Console.WriteLine("Invalid city number.");
            return;
        }

        Console.Write("Enter destination city number: ");
        if (!int.TryParse(Console.ReadLine(), out int endIndex) ||
            endIndex < 1 || endIndex > cities.Count)
        {
            Console.WriteLine("Invalid city number.");
            return;
        }

        City start = cities[startIndex - 1];
        City end = cities[endIndex - 1];

        if (start.Equals(end))
        {
            Console.WriteLine("\nStart and destination are the same city!");
            return;
        }

        DijkstraAlgorithm algorithm = new DijkstraAlgorithm(graph, showSteps);
        algorithm.DisplayShortestPath(start, end);
    }

    static void VisualizeGraph(Graph graph)
    {
        try
        {
            Console.WriteLine("\nOpening graph visualization window...");
            GraphVisualizer visualizer = new GraphVisualizer(graph);
            visualizer.ShowGraph();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError visualizing graph: {ex.Message}");
            Console.WriteLine("Note: Graph visualization requires a graphical environment.");
        }
    }

    static void FindPathAndVisualize(Graph graph)
    {
        List<City> cities = graph.GetAllCities();

        Console.WriteLine("\nAvailable cities:");
        for (int i = 0; i < cities.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {cities[i].Name}");
        }

        Console.Write("\nEnter start city number: ");
        if (!int.TryParse(Console.ReadLine(), out int startIndex) ||
            startIndex < 1 || startIndex > cities.Count)
        {
            Console.WriteLine("Invalid city number.");
            return;
        }

        Console.Write("Enter destination city number: ");
        if (!int.TryParse(Console.ReadLine(), out int endIndex) ||
            endIndex < 1 || endIndex > cities.Count)
        {
            Console.WriteLine("Invalid city number.");
            return;
        }

        City start = cities[startIndex - 1];
        City end = cities[endIndex - 1];

        if (start.Equals(end))
        {
            Console.WriteLine("\nStart and destination are the same city!");
            return;
        }

        DijkstraAlgorithm algorithm = new DijkstraAlgorithm(graph, showSteps: false);
        List<City> path = algorithm.GetShortestPath(start, end);

        if (path.Count == 0)
        {
            Console.WriteLine($"\nNo path exists from {start.Name} to {end.Name}");
            return;
        }

        algorithm.DisplayShortestPath(start, end);

        try
        {
            Console.WriteLine("\nOpening visualization window...");
            GraphVisualizer visualizer = new GraphVisualizer(graph);
            visualizer.ShowGraphWithPath(start, end, path);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError visualizing path: {ex.Message}");
            Console.WriteLine("Note: Graph visualization requires a graphical environment.");
        }
    }
}
