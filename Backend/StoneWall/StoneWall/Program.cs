using Microsoft.EntityFrameworkCore;
using StoneWall.Data;

namespace StoneWall
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            var connString = builder.Configuration.GetConnectionString("Default");
            builder.Services.AddDbContext<StoneWallDbContext>(options =>
            {
                options.UseMySql(connString,ServerVersion.AutoDetect(connString));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
