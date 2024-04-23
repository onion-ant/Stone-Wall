using Microsoft.EntityFrameworkCore;
using StoneWall.Entities;

namespace StoneWall.Data
{
    public class StoneWallDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemStreaming> Item_Streaming { get; set; }
        public DbSet<StreamingService> Streaming_Service { get; set; }
        public StoneWallDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
