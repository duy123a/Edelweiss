using Edelweiss.SearchService.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;

namespace Edelweiss.SearchService.Controllers;

[ApiController]
[Route("api/search")]
public class SearchController : ControllerBase
{
    public async Task<ActionResult<List<Item>>> SearchItems([FromQuery] string? searchTerm)
    {
        var query = DB.Find<Item>();

        query.Sort(x => x.Ascending(a => a.Creator));

        if (!string.IsNullOrEmpty(searchTerm))
        {
            query.Match(Search.Full, searchTerm).SortByTextScore();
        }

        var result = await query.ExecuteAsync();

        return result;
    }
}
