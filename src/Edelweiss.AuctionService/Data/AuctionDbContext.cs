using Edelweiss.AuctionService.Entities;
using Microsoft.EntityFrameworkCore;

namespace Edelweiss.AuctionService.Data;

public class AuctionDbContext : DbContext
{
    public AuctionDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Auction> Auctions { get; set; }
}