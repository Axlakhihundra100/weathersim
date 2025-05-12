using System.Security.Cryptography;

namespace weatherappsim;

using System;
using System.Collections.Generic;

public class WeatherApp
{
    private List<City> Cities;
    private WeatherService WeatherService;
    private SmhiWeatherService SmhiService;

    public WeatherApp()
    {
        WeatherService = new WeatherService();
        SmhiService = new SmhiWeatherService();
        Cities = new List<City>()
        {
            new City("Stockholm", WeatherService.GenerateWeather(), 59.3293, 18.0686),
            new City("Malmö", WeatherService.GenerateWeather(), 55.6050, 13.0038),
            new City("Norrtälje", WeatherService.GenerateWeather(), 59.7577, 18.6986),
            new City("Luleå", WeatherService.GenerateWeather(), 65.5848, 22.1567),
        };
    }

    public void Run()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Weather App Sim");
            DisplayCities();

            Console.WriteLine("\nOptions:");
            Console.WriteLine("1. View Weather");
            Console.WriteLine("2. Simulate Weather Update");
            Console.WriteLine("3. Update with SMHI");
            Console.WriteLine("4. Exit");


            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ViewWeather();
                    break;
                case "2":
                    SimulateWeatherUpdate();
                    break;
                case "3":
                    RealWeatherUpdate().Wait();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid Choice, Press enter");
                    Console.ReadLine();
                    break;
            }
        }
    }

    private void DisplayCities()
    {
        for (int i = 0; i < Cities.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {Cities[i].Name}");
        }
    }

    private void ViewWeather()
    {
        Console.Write("\nEnter city number to view weather: ");
        if (int.TryParse(Console.ReadLine(), out int cityIndex) && cityIndex >= 1 && cityIndex <= Cities.Count)
        {
            var city = Cities[cityIndex - 1];
            Console.WriteLine($"\nWeather in {city.Name}: {city.Weather}");
        }
        else
        {
            Console.WriteLine("Invalid city.");
        }

        Console.WriteLine("\nPress Enter to return to the menu.");
        Console.ReadLine();
    }

    private void SimulateWeatherUpdate()
    {
        foreach (var city in Cities)
        {
            WeatherService.UpdateWeather(city);
        }

        Console.WriteLine("\nWeather data updated.");
        Console.WriteLine("Press enter to return to menu.");
        Console.ReadLine();
    }

    private async Task RealWeatherUpdate()
    {
        foreach (var city in Cities)
        {
            Console.WriteLine($"Fetching weather for {city.Name} ({city.Latitude}, {city.Longitude})...");
            var data = await SmhiService.GetRealWeatherAsync(city.Latitude, city.Longitude);
            if (data != null)
            {
                city.Weather = data;
                Console.WriteLine($" Success: {data}");
            }
            else
            {
                Console.WriteLine("Failed to fetch real weather.");
            }
        }
        Console.WriteLine("\nReal weather data fetched from SMHI.");
        Console.WriteLine("Press enter to return to menu.");
        Console.ReadLine();
    }
}