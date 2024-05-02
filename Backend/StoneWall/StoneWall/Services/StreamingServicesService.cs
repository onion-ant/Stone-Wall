using Microsoft.EntityFrameworkCore;
using StoneWall.Data;
using StoneWall.Entities;
using StoneWall.Helpers;
using StoneWall.Services.Exceptions;
using System.Linq;

namespace StoneWall.Services
{
    public class StreamingServicesService : IStreamingServicesService
    {
        private readonly StoneWallDbContext _context;
        private readonly int _pageSize;
        public StreamingServicesService(StoneWallDbContext context, IConfiguration config)
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
            int totalPages = await GetTotalPages(streamingId);
            if ((totalPages < pageNumber || pageNumber == 0) && totalPages != 0)
            {
                throw new PageException("Invalid pageNumber");
            }
            var streamingItems = await _context.Item_Streaming
                .AsNoTracking()
                .Include(Is => Is.Item)
                .ThenInclude(It => It.Genres)
                .Where(Is => Is.StreamingId == streamingId)
               .Select(Is => new ItemStreaming
               {
                   Item = new Item
                   {
                       TmdbId = Is.Item.TmdbId,
                       Title = Is.Item.Title,
                       OriginalTitle = Is.Item.OriginalTitle,
                       Popularity = Is.Item.Popularity,
                       Type = Is.Item.Type,
                       Genres = Is.Item.Genres
                   },
                   Type = Is.Type,
                   Link = Is.Link,
               })
                .OrderByDescending(Is => Is.Item.Popularity)
                .Skip((pageNumber - 1) * _pageSize)
                .Take(_pageSize)
                .ToListAsync();
            if (!streamingItems.Any())
            {
                throw new NotFoundException($"Theres no registered item from {streamingId}");
            }
            var response = new ItemStreamingPaginationHelper()
            {
                ItemsStreaming = streamingItems,
                LastPage = totalPages,
            };
            return response;
        }
        public async Task<ItemStreamingPaginationHelper> GetItemsByGenreAsync(string streamingId, int pageNumber, int genreId)
        {
            int totalPages = await GetTotalPages(streamingId);
            if ((totalPages < pageNumber || pageNumber == 0) && totalPages != 0)
            {
                throw new PageException("Invalid pageNumber");
            }
            var streamingItems = await _context.Item_Streaming
                .AsNoTracking()
                .Include(Is => Is.Item)
                .ThenInclude(It => It.Genres)
                .Where(Is => Is.StreamingId == streamingId && Is.Item.Genres.Any(g => g.Id == genreId))
               .Select(Is => new ItemStreaming
               {
                   Item = new Item
                   {
                       TmdbId = Is.Item.TmdbId,
                       Title = Is.Item.Title,
                       OriginalTitle = Is.Item.OriginalTitle,
                       Popularity = Is.Item.Popularity,
                       Type = Is.Item.Type,
                       Genres = Is.Item.Genres
                   },
                   Type = Is.Type,
                   Link = Is.Link,
               })
                .OrderByDescending(Is => Is.Item.Popularity)
                .Skip((pageNumber - 1) * _pageSize)
                .Take(_pageSize)
                .ToListAsync();
            if (!streamingItems.Any())
            {
                throw new NotFoundException($"Theres no registered item from {streamingId} with this genre");
            }
            var response = new ItemStreamingPaginationHelper()
            {
                ItemsStreaming = streamingItems,
                LastPage = totalPages,
            };
            return response;
        }
        public async Task<ItemStreamingPaginationHelper> CompareStreamings(string streamingExclusive, string streamingExcluded, int pageNumber)
        {
            int totalPages = await GetTotalPages(streamingExclusive, streamingExcluded);
            if ((totalPages < pageNumber || pageNumber == 0) && totalPages != 0)
            {
                throw new PageException("Invalid pageNumber");
            }
            var ExclusiveItems = await _context.Item_Streaming
                    .Where(Is => Is.StreamingId == streamingExclusive &&
                        !_context.Item_Streaming.Any(Is2 => Is2.StreamingId == streamingExcluded && Is2.Item.TmdbId == Is.Item.TmdbId))
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
            if (!ExclusiveItems.Any())
            {
                throw new NotFoundException($"Theres no items in {streamingExclusive} that dont have in the {streamingExcluded}");
            }
            var response = new ItemStreamingPaginationHelper()
            {
                ItemsStreaming = ExclusiveItems,
                LastPage = totalPages,
            };
            return response;
        }
        private async Task<int> GetTotalPages(string streamingId, string? streamingExcluded = null)
        {
            int totalPages = 0;
            if (string.IsNullOrWhiteSpace(streamingId))
            {
                totalPages = (await _context.Item_Streaming
                .AsNoTracking()
                .CountAsync(Is => Is.StreamingId == streamingId) + _pageSize - 1) / _pageSize;
                return totalPages;
            }
            totalPages = (await _context.Item_Streaming
                .AsNoTracking()
                .CountAsync(Is => Is.StreamingId == streamingId &&
                        !_context.Item_Streaming.Any(Is2 => Is2.StreamingId == streamingExcluded && Is2.Item.TmdbId == Is.Item.TmdbId)) + _pageSize - 1) / _pageSize;
            return totalPages;
        }
    }
}
