using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Edelweiss.WebApi.Controllers;
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
            .Build();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        try
        {
            var result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
            ValidateResult(result);
            return result;
        }
        catch (Exception ex)
        {
            Log.Logger.Verbose("Verbose: {ex}",ex);
            Log.Logger.Debug("Debug: {ex}", ex);
            Log.Logger.Warning("Warning: {ex}", ex);
            Log.Logger.Information("Information: {ex}", ex);
            Log.Logger.Error("Error: {ex}",ex);
            Log.Logger.Fatal("Fatal: {ex}", ex);
            Log.CloseAndFlush();
            return new List<WeatherForecast>();
        }
    }

    private static void ValidateResult(WeatherForecast[] result)
    {
        if (result.Any(a => a.Summary == "Hot" || a.TemperatureC > 30))
            throw new Exception(GetErrorMessage());
    }

    private static string GetErrorMessage()
    {
        return "That shit's hot";
    }
}
