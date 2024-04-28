using Microsoft.EntityFrameworkCore;
using StoneWall.Data;
using StoneWall.Entities;
using StoneWall.Helpers;
using StoneWall.Services.Exceptions;

namespace StoneWall.Services
{
    public class StreamingServicesService : IStreamingServicesService
    {
        private readonly StoneWallDbContext _context;
        private readonly int _pageSize;
        public StreamingServicesService(StoneWallDbContext context,IConfiguration config)
        {
            _context = context;
            _pageSize = int.Parse(config["PageSize"]);
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
        public async Task<List<Addon>> GetAddonsAsync(string streamingId)
        {
            var addons = await _context.Addons.AsNoTracking().Where(Ad => Ad.StreamingService == streamingId).ToListAsync();
            if (!addons.Any())
            {
                throw new NotFoundException("This streaming has no addons");
            }
            return addons;
        }
        public async Task<ItemStreamingPaginationHelper> GetItemsAsync(string streamingId, int pageNumber)
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
                   Link = Is.Link,
               })
                .OrderByDescending(Is => Is.Item.Popularity)
                .Skip((pageNumber - 1) * _pageSize)
                .Take(_pageSize)
                .ToListAsync();
            int totalPages = await GetTotalPages(streamingId);
            if (totalPages < pageNumber)
            {
                throw new LastPageException("Maximum page number exceeded");
            }
            if (!streamingItems.Any())
            {
                throw new NotFoundException("Theres no registered item from this streaming");
            }

            var response = new ItemStreamingPaginationHelper()
            {
                ItemsStreaming = streamingItems,
                LastPage = totalPages,
            };
            return response;
        }

        private async Task<int> GetTotalPages(string streamingId)
        {
            int totalPaginas = (await _context.Item_Streaming
                .AsNoTracking()
                .CountAsync(Is => Is.StreamingId == streamingId) + _pageSize - 1)/_pageSize;
            return totalPaginas;
        }
    }
}
