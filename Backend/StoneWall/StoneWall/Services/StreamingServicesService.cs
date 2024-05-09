using Microsoft.EntityFrameworkCore;
using StoneWall.Data;
using StoneWall.Entities;
using StoneWall.Entities.Enums;
using StoneWall.Helpers;
using StoneWall.Pagination;
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
        public async Task<PagedList<ItemStreaming>> GetItemsAsync(string streamingId, int pageNumber, int offset, int genreId, ItemType? itemType, StreamingType? streamingType)
        {
            if (offset < 1)
            {
                throw new PageException($"Invalid {nameof(offset)}");
            }

            IQueryable<ItemStreaming> query = _context.Item_Streaming
            .AsNoTracking()
            .Where(Is => Is.StreamingId == streamingId)
            .Include(Is => Is.Item);

            if (itemType != null)
            {
                query = query
               .Where(Is => Is.Item.Type == itemType);
            }
            if (streamingType != null)
            {
                query = query
               .Where(Is => Is.Type == streamingType);
            }
            if (genreId != 0)
            {
                query = query
               .Where(Is => Is.Item.Genres.Any(g => g.Id == genreId));
            }

            var streamingItems = query
                .OrderByDescending(Is => Is.Item.Popularity)
                .AsQueryable();
            var streamingItemsPaged = await PagedList<ItemStreaming>.ToPagedListAsync(streamingItems,pageNumber,offset);

            if ((streamingItemsPaged.TotalPages < pageNumber || pageNumber < 1) && streamingItemsPaged.TotalPages != 0)
            {
                throw new PageException($"Invalid {nameof(pageNumber)}");
            }

            if (!streamingItemsPaged.Any())
            {
                throw new NotFoundException($"Theres no registered item from {streamingId}");
            }
            return streamingItemsPaged;
        }
        public async Task<PagedList<ItemStreaming>> CompareStreamings(string streamingExclusive, string streamingExcluded, int pageNumber, int offset, int genreId, ItemType? itemType, StreamingType? streamingType)
        {
            if (offset < 1)
            {
                throw new PageException($"Invalid {nameof(offset)}");
            }

            var excludedTmdbIds = await _context.Item_Streaming
            .Where(Is2 => Is2.StreamingId == streamingExcluded)
            .Select(Is2 => Is2.Item.TmdbId)
            .ToArrayAsync();

            IQueryable<ItemStreaming> query = _context.Item_Streaming
            .Where(Is => Is.StreamingId == streamingExclusive && !excludedTmdbIds.Contains(Is.Item.TmdbId))
            .Include(Is => Is.Item);

            if (itemType != null)
            {
                query = query
                .Where(Is => Is.Item.Type == itemType);
            }
            if (streamingType != null)
            {
                query = query
               .Where(Is => Is.Type == streamingType);
            }
            if (genreId != 0)
            {
                query = query
               .Where(Is => Is.Item.Genres.Any(g => g.Id == genreId));
            }

            var exclusiveItems =  query
                .OrderByDescending(Is => Is.Item.Popularity)
                .AsQueryable();
            var exclusiveItemsPaged = await PagedList<ItemStreaming>.ToPagedListAsync(exclusiveItems, pageNumber, offset);

            if ((exclusiveItemsPaged.TotalPages < pageNumber || pageNumber < 1) && exclusiveItemsPaged.TotalPages != 0)
            {
                throw new PageException($"Invalid {nameof(pageNumber)}");
            }

            if (!exclusiveItemsPaged.Any())
            {
                throw new NotFoundException($"Theres no items in {streamingExclusive} that dont have in the {streamingExcluded}");
            }
            return exclusiveItemsPaged;
        }
        private async Task<int> GetTotalPages(IQueryable<ItemStreaming> query,int offset)
        {
            int count = await query.CountAsync();
            int totalPages = (count + offset - 1) / offset;

            return totalPages;
        }
    }
}
