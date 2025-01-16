using System.Security.Cryptography;

namespace weatherappsim;
using System;
using System.Collections.Generic;

public class WeatherApp
{
    private List<City> Cities;
    private WeatherService WeatherService;

    public WeatherApp()
    {
        WeatherService = new WeatherService();
        Cities = new List<City>
        {
            new City("Stockholm", WeatherService.GenerateWeather()),
            new City("Malmö", WeatherService.GenerateWeather()),
            new City("Norrtälje", WeatherService.GenerateWeather()),
            new City("Luleå", WeatherService.GenerateWeather()),
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
            Console.WriteLine("3. Exit");
            
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
}