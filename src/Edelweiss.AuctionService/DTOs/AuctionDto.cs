namespace Edelweiss.AuctionService.DTOs;

public class AuctionDto
{
    public Guid Id { get; set; }
    public int ReservePrice { get; set; }
    public required string Seller { get; set; }
    public string? Winner { get; set; }
    public int? SoldAmount { get; set; }
    public int? CurrentHighBid { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }
    public DateTime DateAuctionEnd { get; set; }
    public required string Status { get; set; }
    public required string Creator { get; set; }
    public required string Model { get; set; }
    public int Year { get; set; }
    public required string Color { get; set; }
    public string? ImageUrl { get; set; }
}