using System;

namespace Edelweiss.Common;

public class AuctionUpdated
{
    public required string Id { get; set; }
    public string? Creator { get; set; }
    public string? Model { get; set; }
    public int? Year { get; set; }
    public string? Color { get; set; }
    public string? ImageUrl { get; set; }
}
