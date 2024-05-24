using Microsoft.EntityFrameworkCore;
using StoneWall.Entities;

namespace StoneWall.Data
{
    public class StoneWallDbContext : DbContext
    {
        public DbSet<ItemCatalog> ItemsCatalog { get; set; }
        public DbSet<ItemCatalogStreaming> ItemsCatalog_Streamings { get; set; }
        public DbSet<Streaming> Streamings { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserStreaming> User_Streaming { get; set; }
        public DbSet<Addon> Addons { get; set; }
        public StoneWallDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
