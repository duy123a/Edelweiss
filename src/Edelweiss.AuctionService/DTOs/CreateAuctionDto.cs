using System.ComponentModel.DataAnnotations;

namespace Edelweiss.AuctionService.DTOs;

public class CreateAuctionDto
{
    [Required] public required string Creator { get; set; }

    [Required] public required string Model { get; set; }

    [Required] public int Year { get; set; }

    [Required] public required string Color { get; set; }

    [Required] public string? ImageUrl { get; set; }

    [Required] public int ReservePrice { get; set; }

    [Required] public DateTime DateAuctionEnd { get; set; }
}