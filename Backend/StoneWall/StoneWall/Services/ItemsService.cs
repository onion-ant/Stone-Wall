using Microsoft.EntityFrameworkCore;
using StoneWall.Data;
using StoneWall.Entities;
using StoneWall.Entities.Enums;
using StoneWall.Helpers;
using StoneWall.Pagination;
using StoneWall.Services.Exceptions;

namespace StoneWall.Services
{
    public class ItemsService : IItemsService
    {
        private readonly StoneWallDbContext _context;
        public ItemsService(StoneWallDbContext context)
        {
            _context = context;
        }

        public async Task<Item> GetDetailsAsync(int tmdbId)
        {
            var item = await _context.Items.AsNoTracking().Select(It=>new Item()
            {
                TmdbId = It.TmdbId,
                Title = It.Title,
                OriginalTitle = It.OriginalTitle,
                Popularity = It.Popularity,
                Type = It.Type,
                Streamings = It.Streamings.Select(s=>new ItemStreaming()
                {
                    StreamingId = s.StreamingId,
                    Type = s.Type,
                    Link = s.Link,
                }).ToList(),
                Genres = It.Genres.Select(g => new Genre()
                {
                    Id = g.Id,
                    Name = g.Name,
                }).ToList()
            }).FirstOrDefaultAsync(It => It.TmdbId == tmdbId);
                                

            if (item == null)
            {
                throw new NotFoundException($"Theres no items with the id:{tmdbId}");
            }
            return item;
        }

        public async Task<PagedList<Item>> GetItemsAsync(int pageNumber, int offset, int genreId, int atLeast, ItemType? itemType)
        {
            if (offset < 1)
            {
                throw new PageException($"Invalid {nameof(offset)}");
            }

            IQueryable<Item> query = _context.Items
            .AsNoTracking()
            .Where(It => It.Streamings.Count >= atLeast);

            if (itemType != null)
            {
                query = query
               .Where(It => It.Type == itemType);
            }
            if (genreId != 0)
            {
                query = query
               .Where(It => It.Genres.Any(g => g.Id == genreId));
            }

            var items = query
                .OrderByDescending(It => It.Popularity)
                .AsQueryable();
            var pagedItems = await PagedList<Item>.ToPagedList(items,pageNumber,offset);

            if ((pagedItems.TotalPages < pageNumber || pageNumber < 1) && pagedItems.TotalPages != 0)
            {
                throw new PageException($"Invalid {nameof(pageNumber)}");
            }

            if (!pagedItems.Any())
            {
                throw new NotFoundException($"Theres no registered item with this options");
            }
            return pagedItems;
        }
    }
}
