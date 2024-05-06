using Microsoft.EntityFrameworkCore;
using StoneWall.Data;
using StoneWall.Entities;
using StoneWall.Entities.Enums;
using StoneWall.Helpers;
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
                throw new NotFoundException("");
            }
            return item;
        }

        public async Task<ItemPaginationHelper> GetItemsAsync(int pageNumber, int offset, int genreId, int atLeast, ItemType? itemType)
        {
            int totalPages = 0;
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

            totalPages = await GetTotalPages(query, offset);

            if ((totalPages < pageNumber || pageNumber < 1) && totalPages != 0)
            {
                throw new PageException($"Invalid {nameof(pageNumber)}");
            }

            var items = await query
                .Select(It => new Item
                    {
                        TmdbId = It.TmdbId,
                        Title = It.Title,
                        OriginalTitle = It.OriginalTitle,
                        Popularity = It.Popularity,
                        Type = It.Type,
                        Streamings = It.Streamings.Select(s => new ItemStreaming()
                        {
                        StreamingId = s.StreamingId,
                        Type = s.Type,
                        Link = s.Link,
                        }).ToList()
                    })
                .OrderByDescending(It => It.Popularity)
                .Skip((pageNumber - 1) * offset)
                .Take(offset)
                .ToListAsync();

            if (!items.Any())
            {
                throw new NotFoundException($"Theres no registered item with this options");
            }
            var response = new ItemPaginationHelper()
            {
                Items = items,
                LastPage = totalPages,
            };
            return response;
        }
        private async Task<int> GetTotalPages(IQueryable<Item> query, int offset)
        {
            int count = await query.CountAsync();
            int totalPages = (count + offset - 1) / offset;

            return totalPages;
        }
    }
}
