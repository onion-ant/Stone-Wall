using Microsoft.EntityFrameworkCore;
using StoneWall.Data;
using StoneWall.Entities;
using StoneWall.Entities.Enums;
using StoneWall.Helpers;
using StoneWall.Pagination;
using StoneWall.Services.Exceptions;
using System.Linq;
using X.PagedList;

namespace StoneWall.Services
{
    public class StreamingServicesService : IStreamingServicesService
    {
        private readonly StoneWallDbContext _context;
        public StreamingServicesService(StoneWallDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StreamingServices>> GetStreamingsAsync()
        {
            var streamings = await _context.Streaming_Services.AsNoTracking().ToArrayAsync();
            if (!streamings.Any())
            {
                throw new NotFoundException("Theres no registered streamings");
            }
            return streamings;
        }
        public async Task<IEnumerable<Addon>> GetAddonsAsync(string streamingId)
        {
            var addons = await _context.Addons.AsNoTracking().Where(Ad => Ad.StreamingService == streamingId).ToArrayAsync();
            if (!addons.Any())
            {
                throw new NotFoundException("This streaming has no addons");
            }
            return addons;
        }
        public async Task<IPagedList<ItemStreaming>> GetItemsAsync(string streamingId, int pageNumber, int offset, StreamingType? streamingType, ItemParameters itemParams)
        {
            if (offset < 1)
            {
                throw new PageException($"Invalid {nameof(offset)}");
            }

            IQueryable<ItemStreaming> query = _context.Item_Streaming
            .AsNoTracking()
            .Where(Is => Is.StreamingId == streamingId)
            .Include(Is => Is.Item)
            .Select(Is => new ItemStreaming()
            {
                Item = new Item()
                {
                    TmdbId = Is.Item.TmdbId,
                    Genres = Is.Item.Genres,
                    Streamings = Is.Item.Streamings,
                    OriginalTitle = Is.Item.OriginalTitle,
                    Title = Is.Item.Title,
                    Type = Is.Item.Type,
                    Popularity = Is.Item.Popularity,
                },
                Type = Is.Type,
                Link = Is.Link,
            })
            .OrderByDescending(Is => Is.Item.Popularity);

            if (itemParams.itemType != null)
            {
                query = query
               .Where(Is => Is.Item.Type == itemParams.itemType);
            }
            if (streamingType != null)
            {
                query = query
               .Where(Is => Is.Type == streamingType);
            }
            if (itemParams.genreId != null)
            {
                query = query
               .Where(Is => Is.Item.Genres.Any(g => g.Id == itemParams.genreId));
            }

            var streamingItemsPaged = await query.ToPagedListAsync(pageNumber,offset);

            if ((streamingItemsPaged.PageCount < pageNumber || pageNumber < 1) && streamingItemsPaged.PageCount != 0)
            {
                throw new PageException($"Invalid {nameof(pageNumber)}");
            }

            if (!streamingItemsPaged.Any())
            {
                throw new NotFoundException($"Theres no registered item from {streamingId}");
            }
            return streamingItemsPaged;
        }
        public async Task<IPagedList<ItemStreaming>> CompareStreamings(string streamingExclusive, string streamingExcluded, int pageNumber, int offset, StreamingType? streamingType, ItemParameters itemParams)
        {
            if (offset < 1)
            {
                throw new PageException($"Invalid {nameof(offset)}");
            }
            if (pageNumber < 1)
            {
                throw new PageException($"Invalid {nameof(pageNumber)}");
            }

            var excludedTmdbIds = await _context.Item_Streaming
            .Where(Is2 => Is2.StreamingId == streamingExcluded)
            .Select(Is2 => Is2.Item.TmdbId)
            .ToArrayAsync();

            IQueryable<ItemStreaming> query = _context.Item_Streaming
            .Where(Is => Is.StreamingId == streamingExclusive && !excludedTmdbIds.Contains(Is.Item.TmdbId))
            .Include(Is => Is.Item)
            .Select(Is => new ItemStreaming()
                {
                    Item = new Item()
                    {
                        TmdbId = Is.Item.TmdbId,
                        Genres = Is.Item.Genres,
                        Streamings = Is.Item.Streamings,
                        OriginalTitle = Is.Item.OriginalTitle,
                        Title = Is.Item.Title,
                        Type = Is.Item.Type,
                        Popularity = Is.Item.Popularity,
                    },
                    Type = Is.Type,
                    Link = Is.Link,
                })
            .OrderByDescending(Is => Is.Item.Popularity);

            if (itemParams.itemType != null)
            {
                query = query
                .Where(Is => Is.Item.Type == itemParams.itemType);
            }
            if (streamingType != null)
            {
                query = query
               .Where(Is => Is.Type == streamingType);
            }
            if (itemParams.genreId != null)
            {
                query = query
               .Where(Is => Is.Item.Genres.Any(g => g.Id == itemParams.genreId));
            }

            var exclusiveItemsPaged = await query.ToPagedListAsync(pageNumber, offset);

            if (exclusiveItemsPaged.PageCount < pageNumber && exclusiveItemsPaged.PageCount != 0)
            {
                throw new PageException($"Invalid {nameof(pageNumber)}");
            }

            if (!exclusiveItemsPaged.Any())
            {
                throw new NotFoundException($"Theres no items in {streamingExclusive} that dont have in the {streamingExcluded}");
            }
            return exclusiveItemsPaged;
        }
    }
}
