using Microsoft.EntityFrameworkCore;
using StoneWall.Data;
using StoneWall.Entities;
using StoneWall.Entities.Enums;
using StoneWall.DTOs.ExternalApiDTOs;
using StoneWall.Pagination;
using StoneWall.Services.Exceptions;
using System.Linq;
using X.PagedList;
using StoneWall.DTOs.RequestDTOs;

namespace StoneWall.Services
{
    public class StreamingService : IStreamingServicesService
    {
        private readonly StoneWallDbContext _context;
        public StreamingService(StoneWallDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Streaming>> GetStreamingsAsync()
        {
            var streamings = await _context.Streamings.AsNoTracking().ToArrayAsync();
            if (!streamings.Any())
            {
                throw new NotFoundException("Theres no registered streamings");
            }
            return streamings;
        }
        public async Task<IEnumerable<Addon>> GetAddonsAsync(string streamingId)
        {
            var addons = await _context.Addons.AsNoTracking().Where(Ad => Ad.StreamingId == streamingId).ToArrayAsync();
            if (!addons.Any())
            {
                throw new NotFoundException("This streaming has no addons");
            }
            return addons;
        }
        public async Task<CursorList<ItemCatalogStreaming>> GetItemsAsync(string streamingId, string? cursor, int limit, StreamingType? streamingType, ItemCatalogStreamingRequestDTO itemParams)
        {
            if (limit < 1)
            {
                throw new PageException($"Invalid {nameof(limit)}");
            }

            IQueryable<ItemCatalogStreaming> query = _context.ItemsCatalog_Streamings
            .Where(Is => Is.StreamingId == streamingId)
            .Include(Is => Is.Item)
            .ThenInclude(It=> It.Genres)
            .Include(Is => Is.Item)
            .ThenInclude(It => It.Streamings)
            .Where(Is => Is.Item.Rating > itemParams.minRating)
            .Where(Is => Is.Price > itemParams.minPrice)
            .OrderByDescending(Is => Is.Item.Rating);

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
            if (itemParams.maxPrice != 0)
            {
                query = query
               .Where(Is => Is.Price <= itemParams.maxPrice);
            }
            if (itemParams.maxRating != 0)
            {
                query = query
               .Where(Is => Is.Item.Rating <= itemParams.maxRating);
            }
            if (cursor != null)
            {
                double popularityCursor = double.Parse(cursor.Split(';')[0]);
                string tmdbidCursor = cursor.Split(';')[1];
                query = query
                .Where(Is => Is.Item.Rating < popularityCursor);
            }

            var streamingItemsPaged = await CursorList<ItemCatalogStreaming>.ToCursorListAsync(query, limit);

            if (!streamingItemsPaged.Any())
            {
                throw new NotFoundException($"Theres no registered item with this options");
            }

            string nextCursor = streamingItemsPaged.Last().Item.Rating.ToString() + ';' + streamingItemsPaged.Last().Item.TmdbId;

            streamingItemsPaged.NextCursor = nextCursor;

            return streamingItemsPaged;
        }
        public async Task<CursorList<ItemCatalogStreaming>> CompareStreamings(string streamingExclusive, string streamingExcluded, string? cursor, int limit, StreamingType? streamingType, ItemCatalogStreamingRequestDTO itemParams)
        {
            if (limit < 1)
            {
                throw new PageException($"Invalid {nameof(limit)}");
            }

            var excludedTmdbIds = await _context.ItemsCatalog_Streamings
            .Where(Is2 => Is2.StreamingId == streamingExcluded)
            .Select(Is2 => Is2.Item.TmdbId)
            .ToArrayAsync();

            IQueryable<ItemCatalogStreaming> query = _context.ItemsCatalog_Streamings
            .Where(Is => Is.StreamingId == streamingExclusive && !excludedTmdbIds.Contains(Is.Item.TmdbId))
            .Include(Is => Is.Item)
            .ThenInclude(It => It.Genres)
            .Include(Is => Is.Item)
            .ThenInclude(It => It.Streamings)
            .Where(Is => Is.Item.Rating > itemParams.minRating)
            .Where(Is => Is.Price > itemParams.minPrice)
            .OrderByDescending(Is => Is.Item.Rating);

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
            if(itemParams.maxPrice != 0)
            {
                query = query
               .Where(Is => Is.Price <= itemParams.maxPrice);
            }
            if(itemParams.maxRating != 0)
            {
                query = query
               .Where(Is => Is.Item.Rating <= itemParams.maxRating);
            }
            if (cursor != null)
            {
                double popularityCursor = double.Parse(cursor.Split(';')[0]);
                string tmdbidCursor = cursor.Split(';')[1];
                query = query
                .Where(Is => Is.Item.Rating < popularityCursor);
            }

            var exclusiveItemsPaged = await CursorList<ItemCatalogStreaming>.ToCursorListAsync(query, limit);


            if (!exclusiveItemsPaged.Any())
            {
                throw new NotFoundException($"Theres no registered item with this options");
            }

            string nextCursor = exclusiveItemsPaged.Last().Item.Rating.ToString() + ';' + exclusiveItemsPaged.Last().Item.TmdbId;

            exclusiveItemsPaged.NextCursor = nextCursor;

            return exclusiveItemsPaged;
        }
    }
}
