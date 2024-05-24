using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StoneWall.Data;
using StoneWall.Entities;
using StoneWall.Extensions;
using StoneWall.Extensions.Mappings;
using StoneWall.Services;
using System.Text.Json.Serialization;

namespace StoneWall
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            } 
            );

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var connString = builder.Configuration.GetConnectionString("Default");
            builder.Services.AddDbContext<StoneWallDbContext>(options =>
            {
                options.UseMySql(connString,ServerVersion.AutoDetect(connString));
            });
            builder.Services.AddHttpClient();
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });
            builder.Services.AddScoped<IStreamingServicesService, StreamingService>();
            builder.Services.AddScoped<ITmdbService,TmdbService>();
            builder.Services.AddScoped<IItemsService, ItemsService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.ConfigureExceptionHandler();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
