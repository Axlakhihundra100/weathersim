using System.Text.Json;

namespace weatherappsim;

public class SmhiWeatherService
{
    private static readonly HttpClient client = new HttpClient();
    
    public async Task<weatherdata?> GetRealWeatherAsync(double lat, double lon)
    {
        try
        {
            string url =
                $"https://opendata-download-metfcst.smhi.se/api/category/pmp3g/version/2/geotype/point/lon/{{lon}}/lat/{{lat}}/data.json\n";
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode(); 
            var json = await response.Content.ReadAsStringAsync();
            using var document = JsonDocument.Parse(json);
            var timeSeries = document.RootElement.GetProperty("timeSeries")[0];
            double temp = 0;
            int humidity = 0;
            string condition = "Unknown";

            foreach (var param in timeSeries.GetProperty("parameters").EnumerateArray())
            {
                string name = param.GetProperty("name").GetString();
                var value = param.GetProperty("values")[0].GetDouble();
                if (name == "t") temp = value;
                else if (name == "r") humidity = (int)value;
                else if (name == "Wsymb2") condition = MapConditionCode((int)value);
            }

            return new weatherdata(temp, humidity, condition);
        }
        catch
        {
            return null;
        }
    }

    private string MapConditionCode(int code)
    {
        return code switch
        {
            1 => "Clear",
            2 => "Partly Cloudy",
            3 => "Cloudy",
            4 or 5 => "Rainy",
            6 or 7 => "Snowy",
            8 => "Stormy",
            _ => "Unknown"
        };
    }
}
