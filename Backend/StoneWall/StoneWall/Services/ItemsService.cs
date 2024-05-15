using Microsoft.EntityFrameworkCore;
using StoneWall.Data;
using StoneWall.Entities;
using StoneWall.Entities.Enums;
using StoneWall.Helpers;
using StoneWall.Pagination;
using StoneWall.Services.Exceptions;
using X.PagedList;

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
            var item = await _context.Items.AsNoTracking()
            .Select(It => new Item()
                {
                Title = It.Title,
                Genres = It.Genres.Select(g=>new Genre()
                {
                    Id = g.Id,
                    Name = g.Name,
                }).ToList(),
                OriginalTitle = It.OriginalTitle,
                Popularity = It.Popularity,
                Streamings = It.Streamings,
                Type = It.Type,
                TmdbId  = It.TmdbId,
                })
            .FirstOrDefaultAsync(It => It.TmdbId == tmdbId);
                                

            if (item == null)
            {
                throw new NotFoundException($"Theres no items with the id:{tmdbId}");
            }
            return item;
        }

        public async Task<IPagedList<Item>> GetItemsAsync(int offset, int pageNumber, ItemParameters itemParams)
        {
            if (offset < 1)
            {
                throw new PageException($"Invalid {nameof(offset)}");
            }
            if (pageNumber < 1)
            {
                throw new PageException($"Invalid {nameof(pageNumber)}");
            }

            IQueryable<Item> query = _context.Items
            .AsNoTracking()
            .Where(It => It.Streamings.Count >= itemParams.atLeast)
            .Include(It => It.Streamings)
            .OrderByDescending(It => It.Popularity);

            if (itemParams.itemType != null)
            {
                query = query
               .Where(It => It.Type == itemParams.itemType);
            }
            if (itemParams.genreId != null)
            {
                query = query
               .Where(It => It.Genres.Any(g => g.Id == itemParams.genreId));
            }

            var pagedItems = await query.ToPagedListAsync(pageNumber, offset);

            if (pagedItems.PageCount < pageNumber && pagedItems.PageCount != 0)
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
