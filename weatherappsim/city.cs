namespace weatherappsim;

public class City
{
    public string Name { get; set; }
    public weatherdata Weather { get; set; }

    public City(string name, weatherdata weather)
    {
        Name = name;
        Weather = weather;
    }
}