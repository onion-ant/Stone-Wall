using Microsoft.EntityFrameworkCore;
using StoneWall.Data;
using StoneWall.Entities;
using StoneWall.Services.Exceptions;

namespace StoneWall.Services
{
    public class StreamingServicesService : IStreamingServicesService
    {
        private readonly StoneWallDbContext _context;
        public StreamingServicesService(StoneWallDbContext context)
        {
            _context = context;
        }
        public async Task<List<StreamingServices>> GetStreamingsAsync()
        {
            var streamings = await _context.Streaming_Services.AsNoTracking().ToListAsync();
            if (!streamings.Any())
            {
                throw new NotFoundException("Theres no registered streamings");
            }
            return streamings;
        }
        public async Task<List<ItemStreaming>> GetItemsAsync(string streamingId)
        {
            var streamingItems = await _context.Item_Streaming.AsNoTracking().Include(Is => Is.Item).Where(Is => Is.StreamingId == streamingId)
               .Select(Is => new ItemStreaming
               {
                   Item = new Item
                   {
                       TmdbId = Is.Item.TmdbId,
                       Title = Is.Item.Title,
                       OriginalTitle = Is.Item.OriginalTitle,
                       Popularity = Is.Item.Popularity,
                       Type = Is.Item.Type
                   },
                   Type = Is.Type,
                   Link = Is.Link
               }).ToListAsync();
            if (!streamingItems.Any())
            {
                throw new NotFoundException("Theres no registered item from this streaming");
            }
            return streamingItems;
        }
    }
}
