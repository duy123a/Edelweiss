using Edelweiss.SearchService.Models;
using MongoDB.Entities;
using System;

namespace Edelweiss.SearchService.Services;

public class AuctionSvcHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public AuctionSvcHttpClient(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    public async Task<List<Item>> GetItemForSearchDb()
    {
        var lastUpdated = await DB.Find<Item, string>()
            .Sort(x => x.Descending(x => x.DateUpdated))
            .Project(x => x.DateUpdated.ToString())
            .ExecuteFirstAsync();

        var result = await _httpClient.GetFromJsonAsync<List<Item>>(_config["AuctionServiceUrl"]
            + "/api/auctions?date=" + lastUpdated);
        return result ?? new List<Item>();
    }
}
