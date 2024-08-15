using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

class Program
{
    private static readonly HttpClient client = new HttpClient();

    static async Task Main(string[] args)
    {
        string apiKey = "e6aa3cef39038203d4cd3847809efaa6";
        string city = "Dushanbe";
        string url = $"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";

        try
        {
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            JObject weatherData = JObject.Parse(responseBody);

            string cityName = weatherData["name"].ToString();
            string temperature = weatherData["main"]["temp"].ToString();
            string weatherDescription = weatherData["weather"][0]["description"].ToString();

            Console.WriteLine($"Город: {cityName}");
            Console.WriteLine($"Температура: {temperature}°C");
            Console.WriteLine($"Описание: {weatherDescription}");
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("Ошибка при получении данных о погоде: {0}", e.Message);
        }
    }
}
