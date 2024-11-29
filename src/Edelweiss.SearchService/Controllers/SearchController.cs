using Edelweiss.SearchService.Models;
using Edelweiss.SearchService.RequestHelpers;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;

namespace Edelweiss.SearchService.Controllers;

[ApiController]
[Route("api/search")]
public class SearchController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Item>>> SearchItems([FromQuery] SearchParams searchParams)
    {
        var query = DB.PagedSearch<Item, Item>();

        if (!string.IsNullOrEmpty(searchParams.SearchTerm))
        {
            query.Match(Search.Full, searchParams.SearchTerm).SortByTextScore();
        }

        query = searchParams.OrderBy switch
        {
            "creator" => query.Sort(x => x.Ascending(a => a.Creator)),
            "make" => query.Sort(x => x.Ascending(a => a.Creator)),
            "new" => query.Sort(x => x.Descending(a => a.DateCreated)),
            _ => query.Sort(x => x.Ascending(a => a.DateAuctionEnd)),
        };

        query = searchParams.FilterBy switch
        {
            "finished" => query.Match(x => x.DateAuctionEnd < DateTime.UtcNow),
            "endingSoon" => query.Match(x => x.DateAuctionEnd < DateTime.UtcNow.AddHours(6)
                && x.DateAuctionEnd > DateTime.UtcNow),
            // For this example it will be lower than datetime utcnow due to old records
            _ => query.Match(x => x.DateAuctionEnd < DateTime.UtcNow)
        };

        if (!string.IsNullOrEmpty(searchParams.Seller))
        {
            query = query.Match(x => x.Seller == searchParams.Seller);
        }

        if (!string.IsNullOrEmpty(searchParams.Winner))
        {
            query = query.Match(x => x.Winner == searchParams.Winner);
        }

        query.PageNumber(searchParams.PageNumber);
        query.PageSize(searchParams.PageSize);

        var result = await query.ExecuteAsync();

        return Ok(new
        {
            results = result.Results,
            pageCount = result.PageCount,
            totalCount = result.TotalCount
        });
    }
}
