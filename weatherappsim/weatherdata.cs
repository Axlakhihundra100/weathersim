namespace weatherappsim;

public class weatherdata
{
    public double Temperature { get; set;  }
    public int Humidity { get; set; }
    public string Condition { get; set; }

    public weatherdata(double temperature, int humidity, string condition)
    {
        Temperature = temperature;
        Humidity = humidity;
        Condition = condition;
    }

    public override string ToString()
    {
        return $"Temp: {Temperature:F1}°C, Humidity:{Humidity}%, Condition:{Condition}";
    }
}