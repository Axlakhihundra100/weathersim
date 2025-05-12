namespace weatherappsim;

public class City
{
    public string Name { get; set; }
    public weatherdata Weather { get; set; }
    
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public City(string name, weatherdata weather, double lat, double lon)
    {
        Name = name;
        Weather = weather;
        Latitude = lat;
        Longitude = lon;
    }
}