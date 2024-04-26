using Microsoft.EntityFrameworkCore;
using StoneWall.Entities;

namespace StoneWall.Data
{
    public class StoneWallDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemStreaming> Item_Streaming { get; set; }
        public DbSet<StreamingService> Streaming_Services { get; set; }
        public DbSet<ItemGenre> itemGenre { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserStreaming> User_Streaming { get; set; }
        public StoneWallDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
