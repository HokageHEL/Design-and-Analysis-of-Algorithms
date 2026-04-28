using System.Diagnostics;
using System.Text;

namespace CityRoutePlanner;

public class GraphVisualizer
{
    private Graph cityGraph;

    public GraphVisualizer(Graph cityGraph)
    {
        this.cityGraph = cityGraph;
    }

    public void ShowGraph()
    {
        var cities = cityGraph.GetAllCities();
        var addedEdges = new HashSet<string>();
        var nodes = new List<(string name, string color)>();
        var edges = new List<(string from, string to, int distance, string color, int width)>();

        foreach (var city in cities)
        {
            nodes.Add((city.Name, "lightblue"));

            var roads = cityGraph.GetRoadsFrom(city);
            foreach (var road in roads)
            {
                string edgeKey1 = $"{road.From.Name}-{road.To.Name}";
                string edgeKey2 = $"{road.To.Name}-{road.From.Name}";

                if (!addedEdges.Contains(edgeKey1) && !addedEdges.Contains(edgeKey2))
                {
                    edges.Add((road.From.Name, road.To.Name, road.Distance, "#666", 2));
                    addedEdges.Add(edgeKey1);
                }
            }
        }

        string html = GenerateHtml("City Network", nodes, edges);
        string filename = "city_network.html";
        File.WriteAllText(filename, html);

        Console.WriteLine($"Graph saved to {filename}");
        OpenInBrowser(filename);
    }

    public void ShowGraphWithPath(City start, City end, List<City> path)
    {
        var cities = cityGraph.GetAllCities();
        var addedEdges = new HashSet<string>();
        var pathSet = new HashSet<string>();
        var nodes = new List<(string name, string color)>();
        var edges = new List<(string from, string to, int distance, string color, int width)>();

        for (int i = 0; i < path.Count - 1; i++)
        {
            pathSet.Add($"{path[i].Name}-{path[i + 1].Name}");
            pathSet.Add($"{path[i + 1].Name}-{path[i].Name}");
        }

        foreach (var city in cities)
        {
            string color;
            if (city.Equals(start))
                color = "lightgreen";
            else if (city.Equals(end))
                color = "#ff6b6b";
            else if (path.Any(c => c.Name == city.Name))
                color = "yellow";
            else
                color = "lightblue";

            nodes.Add((city.Name, color));

            var roads = cityGraph.GetRoadsFrom(city);
            foreach (var road in roads)
            {
                string edgeKey1 = $"{road.From.Name}-{road.To.Name}";
                string edgeKey2 = $"{road.To.Name}-{road.From.Name}";

                if (!addedEdges.Contains(edgeKey1) && !addedEdges.Contains(edgeKey2))
                {
                    bool inPath = pathSet.Contains(edgeKey1);
                    edges.Add((road.From.Name, road.To.Name, road.Distance,
                              inPath ? "red" : "#666", inPath ? 4 : 2));
                    addedEdges.Add(edgeKey1);
                }
            }
        }

        string html = GenerateHtml($"Shortest Path: {start.Name} → {end.Name}", nodes, edges);
        string filename = $"path_{start.Name}_to_{end.Name}.html";
        File.WriteAllText(filename, html);

        Console.WriteLine($"Path visualization saved to {filename}");
        OpenInBrowser(filename);
    }

    private string GenerateHtml(string title, List<(string name, string color)> nodes,
                                List<(string from, string to, int distance, string color, int width)> edges)
    {
        var sb = new StringBuilder();
        sb.AppendLine("<!DOCTYPE html>");
        sb.AppendLine("<html>");
        sb.AppendLine("<head>");
        sb.AppendLine($"<title>{title}</title>");
        sb.AppendLine("<style>");
        sb.AppendLine("body { margin: 0; font-family: Arial, sans-serif; }");
        sb.AppendLine("#graph { width: 100vw; height: 100vh; }");
        sb.AppendLine("</style>");
        sb.AppendLine("<script src=\"https://unpkg.com/vis-network/standalone/umd/vis-network.min.js\"></script>");
        sb.AppendLine("</head>");
        sb.AppendLine("<body>");
        sb.AppendLine($"<div id=\"graph\"></div>");
        sb.AppendLine("<script>");

        sb.AppendLine("var nodes = new vis.DataSet([");
        foreach (var node in nodes)
        {
            sb.AppendLine($"  {{ id: '{node.name}', label: '{node.name}', color: '{node.color}', " +
                         "shape: 'circle', font: { size: 16 } },");
        }
        sb.AppendLine("]);");

        sb.AppendLine("var edges = new vis.DataSet([");
        foreach (var edge in edges)
        {
            sb.AppendLine($"  {{ from: '{edge.from}', to: '{edge.to}', label: '{edge.distance} km', " +
                         $"color: {{ color: '{edge.color}' }}, width: {edge.width}, " +
                         "font: { size: 14, align: 'middle' } },");
        }
        sb.AppendLine("]);");

        sb.AppendLine("var container = document.getElementById('graph');");
        sb.AppendLine("var data = { nodes: nodes, edges: edges };");
        sb.AppendLine("var options = {");
        sb.AppendLine("  physics: {");
        sb.AppendLine("    enabled: true,");
        sb.AppendLine("    stabilization: { iterations: 200 },");
        sb.AppendLine("    barnesHut: { gravitationalConstant: -8000, springLength: 250, springConstant: 0.04 }");
        sb.AppendLine("  },");
        sb.AppendLine("  edges: { smooth: { type: 'continuous' } },");
        sb.AppendLine("  nodes: { size: 30 }");
        sb.AppendLine("};");
        sb.AppendLine("var network = new vis.Network(container, data, options);");
        sb.AppendLine("</script>");
        sb.AppendLine("</body>");
        sb.AppendLine("</html>");

        return sb.ToString();
    }

    private void OpenInBrowser(string filename)
    {
        try
        {
            string fullPath = Path.GetFullPath(filename);

            if (OperatingSystem.IsWindows())
            {
                Process.Start(new ProcessStartInfo(fullPath) { UseShellExecute = true });
            }
            else if (OperatingSystem.IsMacOS())
            {
                Process.Start("open", fullPath);
            }
            else if (OperatingSystem.IsLinux())
            {
                Process.Start("xdg-open", fullPath);
            }

            Console.WriteLine($"Opening {filename} in your default browser...");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Could not open browser automatically: {ex.Message}");
            Console.WriteLine($"Please open {filename} manually in your browser.");
        }
    }
}
