namespace CityRoutePlanner;

public class Road
{
    public City From { get; set; }
    public City To { get; set; }
    public int Distance { get; set; }

    public Road(City from, City to, int distance)
    {
        From = from;
        To = to;
        Distance = distance;
    }

    public override string ToString()
    {
        return $"{From.Name} -> {To.Name} ({Distance} km)";
    }
}
