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
        public async Task<CursorList<ItemStreaming>> GetItemsAsync(string streamingId, string? cursor, int limit, StreamingType? streamingType, ItemParameters itemParams)
        {
            if (limit < 1)
            {
                throw new PageException($"Invalid {nameof(limit)}");
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
                Type = Is.Type
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
            if (itemParams.name != null)
            {
                query = query
               .Where(It => It.Item.Title.ToLower().Contains(itemParams.name.ToLower()) || It.Item.OriginalTitle.ToLower().Contains(itemParams.name.ToLower()));
            }
            if(cursor != null)
            {
                double popularityCursor = double.Parse(cursor.Split(';')[0]);
                int tmdbidCursor = int.Parse(cursor.Split(';')[1]);
                query = query
                .Where(Is => Is.Item.Popularity < popularityCursor || Is.Item.Popularity == popularityCursor && Is.Item.TmdbId > tmdbidCursor);
            }

            var streamingItemsPaged = await CursorList<ItemStreaming>.ToCursorListAsync(query, limit);

            string nextCursor = streamingItemsPaged.Last().Item.Popularity.ToString() + ';' + streamingItemsPaged.Last().Item.TmdbId;

            streamingItemsPaged.NextCursor = nextCursor;

            if (!streamingItemsPaged.Any())
            {
                throw new NotFoundException($"Theres no registered item with this options");
            }
            return streamingItemsPaged;
        }
        public async Task<CursorList<ItemStreaming>> CompareStreamings(string streamingExclusive, string streamingExcluded, string? cursor, int limit, StreamingType? streamingType, ItemParameters itemParams)
        {
            if (limit < 1)
            {
                throw new PageException($"Invalid {nameof(limit)}");
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
            if (itemParams.name != null)
            {
                query = query
               .Where(It => It.Item.Title.ToLower().Contains(itemParams.name.ToLower()) || It.Item.OriginalTitle.ToLower().Contains(itemParams.name.ToLower()));
            }
            if (cursor != null)
            {
                double popularityCursor = double.Parse(cursor.Split(';')[0]);
                int tmdbidCursor = int.Parse(cursor.Split(';')[1]);
                query = query
                .Where(Is => Is.Item.Popularity < popularityCursor || Is.Item.Popularity == popularityCursor && Is.Item.TmdbId > tmdbidCursor);
            }

            var exclusiveItemsPaged = await CursorList<ItemStreaming>.ToCursorListAsync(query, limit);

            string nextCursor = exclusiveItemsPaged.Last().Item.Popularity.ToString() + ';' + exclusiveItemsPaged.Last().Item.TmdbId;

            exclusiveItemsPaged.NextCursor = nextCursor;

            if (!exclusiveItemsPaged.Any())
            {
                throw new NotFoundException($"Theres no registered item with this options");
            }
            return exclusiveItemsPaged;
        }
    }
}
