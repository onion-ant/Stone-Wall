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
        public async Task<List<Addon>> GetAddonsAsync(string streamingId)
        {
            var addons = await _context.Addons.AsNoTracking().Where(Ad => Ad.StreamingService == streamingId).ToListAsync();
            if (!addons.Any())
            {
                throw new NotFoundException("This streaming has no addons");
            }
            return addons;
        }
        public async Task<ItemStreamingPaginationHelper> GetItemsAsync(string streamingId, int pageNumber, int offset)
        {
            if (offset < 1)
            {
                throw new PageException($"Invalid {nameof(offset)}");
            }
            int totalPages = await GetTotalPages(streamingId,offset);
            if ((totalPages < pageNumber || pageNumber < 1) && totalPages != 0)
            {
                throw new PageException($"Invalid {nameof(pageNumber)}");
            }
            var streamingItems = await _context.Item_Streaming
                .AsNoTracking()
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
                   },
                   Type = Is.Type,
                   Link = Is.Link,
               })
                .OrderByDescending(Is => Is.Item.Popularity)
                .Skip((pageNumber - 1) * offset)
                .Take(offset)
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
        public async Task<ItemStreamingPaginationHelper> GetItemsByGenreAsync(string streamingId, int pageNumber, int offset, int genreId)
        {
            if (offset < 1)
            {
                throw new PageException($"Invalid {nameof(offset)}");
            }
            int totalPages = await GetTotalPages(streamingId, offset, genreId:genreId);
            if ((totalPages < pageNumber || pageNumber < 1) && totalPages != 0)
            {
                throw new PageException($"Invalid {nameof(pageNumber)}");
            }
            var streamingItems = await _context.Item_Streaming
                .AsNoTracking()
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
                   },
                   Type = Is.Type,
                   Link = Is.Link,
               })
                .OrderByDescending(Is => Is.Item.Popularity)
                .Skip((pageNumber - 1) * offset)
                .Take(offset)
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
        public async Task<ItemStreamingPaginationHelper> CompareStreamings(string streamingExclusive, string streamingExcluded, int pageNumber,int offset)
        {
            if (offset < 1)
            {
                throw new PageException($"Invalid {nameof(offset)}");
            }
            int totalPages = await GetTotalPages(streamingExclusive, offset, streamingExcluded);
            if ((totalPages < pageNumber || pageNumber < 1) && totalPages != 0)
            {
                throw new PageException($"Invalid {nameof(pageNumber)}");
            }
            var excludedTmdbIds = await _context.Item_Streaming
            .Where(Is2 => Is2.StreamingId == streamingExcluded)
            .Select(Is2 => Is2.Item.TmdbId)
            .ToListAsync();

            var exclusiveItems = await _context.Item_Streaming
                .Where(Is => Is.StreamingId == streamingExclusive && !excludedTmdbIds.Contains(Is.Item.TmdbId))
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
                .Skip((pageNumber - 1) * offset)
                .Take(offset)
                .ToListAsync();
            if (!exclusiveItems.Any())
            {
                throw new NotFoundException($"Theres no items in {streamingExclusive} that dont have in the {streamingExcluded}");
            }
            var response = new ItemStreamingPaginationHelper()
            {
                ItemsStreaming = exclusiveItems,
                LastPage = totalPages,
            };
            return response;
        }
        private async Task<int> GetTotalPages(string streamingId, int offset, string? streamingExcluded = null, int? genreId = null)
        {
            IQueryable<ItemStreaming> query = _context.Item_Streaming.AsNoTracking().Where(Is => Is.StreamingId == streamingId);

            if (streamingExcluded != null)
            {
                query = query.Where(Is => !_context.Item_Streaming.Any(Is2 => Is2.StreamingId == streamingExcluded && Is2.Item.TmdbId == Is.Item.TmdbId));
            }
            else if (genreId != null)
            {
                query = query.Where(Is => Is.Item.Genres.Any(g => g.Id == genreId));
            }

            int count = await query.CountAsync();
            int totalPages = (count + offset - 1) / offset;

            return totalPages;
        }
    }
}
