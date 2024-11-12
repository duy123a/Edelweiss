using Edelweiss.SearchService.Models;
using MongoDB.Driver;
using MongoDB.Entities;
using System.Text.Json;

namespace Edelweiss.SearchService.Data;

public class DbInitializer
{
    public static async Task InitDb(WebApplication app)
    {
        await DB.InitAsync(
            "SearchDb",
            MongoClientSettings.FromConnectionString(app.Configuration.GetConnectionString("MongoDbConnection")));

        await DB.Index<Item>()
            .Key(x => x.Creator, KeyType.Text)
            .Key(x => x.Model, KeyType.Text)
            .Key(x => x.Color, KeyType.Text)
            .CreateAsync();

        var count = await DB.CountAsync<Item>();

        if (count == 0)
        {
            Console.WriteLine("No data - will attempt to seed");
            var itemData = await File.ReadAllTextAsync("Data/auctions.json");
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var items = JsonSerializer.Deserialize<List<Item>>(itemData, options);
            if (items != null)
            {
                await DB.SaveAsync(items);
            }
        }
    }
}