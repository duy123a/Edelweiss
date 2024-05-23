using System.ComponentModel.DataAnnotations.Schema;

namespace Edelweiss.AuctionService.Entities;

// As it is related (1-1) to auction, it will be created as we create the Auction table by Entity, but the name won't be pluralized
// So we need explicitly write a name here
[Table("Items")]
public class Item
{
    public Guid Id { get; set; }
    public required string Creator { get; set; }
    public required string Model { get; set; }
    public int Year { get; set; }
    public required string Color { get; set; }
    public string? ImageUrl { get; set; }

    // nav properties
    public Auction? Auction { get; set; }
    public Guid AuctionId { get; set; }
}
