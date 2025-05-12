namespace weatherappsim;
using System;
public class WeatherService
{
    private static readonly Random Random = new Random();
    private static readonly string[] Conditions = { "Sunny", "Cloudy", "Rainy", "Snowy", "Windy", "Stormy" };

    public weatherdata GenerateWeather()
    {
        double temperature = Random.NextDouble() * 40 - 10; // -10c till 30c
        int humidity = Random.Next(20, 100); // 20% till 100%
        string condition = Conditions[Random.Next(Conditions.Length)];

        if (temperature < 0 && condition == "Rainy")
        {
            condition = "Snowy";
        }
        
        return new weatherdata(temperature, humidity, condition);
    }

    public void UpdateWeather(City city)
    {
        city.Weather = GenerateWeather();
    }
}