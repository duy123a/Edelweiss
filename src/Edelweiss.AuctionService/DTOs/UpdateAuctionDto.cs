namespace Edelweiss.AuctionService.DTOs;

public class UpdateAuctionDto
{
    public required string Creator { get; set; }
    public required string Model { get; set; }
    public int Year { get; set; }
    public required string Color { get; set; }
    public string? ImageUrl { get; set; }
}
