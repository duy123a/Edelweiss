namespace Edelweiss.AuctionService.Entities;

public class Auction
{
    public Guid Id { get; set; }
    public int ReservePrice { get; set; } = 0;
    public required string Seller { get; set; }
    public string? Winner { get; set; }
    public int? SoldAmount { get; set; }
    public int? CurrentHighBid { get; set; }
    public DateTimeOffset DateCreated { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset DateUpdated { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset DateAuctionEnd { get; set; }
    public Status Status { get; set; }
    public required Item Item { get; set; }
}
