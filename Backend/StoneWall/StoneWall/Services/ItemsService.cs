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

        public async Task<CursorList<Item>> GetItemsAsync(int limit, string? cursor, ItemParameters itemParams)
        {
            if (limit < 1)
            {
                throw new PageException($"Invalid {nameof(limit)}");
            }

            IQueryable<Item> query = _context.Items
            .AsNoTracking()
            .Where(It => It.Streamings.Count >= itemParams.atLeast)
            .Include(It => It.Streamings)
            .OrderByDescending(It => It.Popularity)
            .ThenBy(It => It.TmdbId);

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
            if(itemParams.name != null)
            {
                query = query
               .Where(It => It.Title.ToLower().Contains(itemParams.name.ToLower()) || It.OriginalTitle.ToLower().Contains(itemParams.name.ToLower()));
            }
            if(cursor != null)
            {
                double popularityCursor = double.Parse(cursor.Split(';')[0]);
                int tmdbidCursor = int.Parse(cursor.Split(';')[1]);
                query = query
                .Where(It => It.Popularity < popularityCursor || It.Popularity == popularityCursor && It.TmdbId > tmdbidCursor);
            }

            var pagedItems = await CursorList<Item>.ToCursorListAsync(query,limit);

            if (!pagedItems.Any())
            {
                throw new NotFoundException($"Theres no registered item with this options");
            }

            string nextCursor = pagedItems.Last().Popularity.ToString() + ';' + pagedItems.Last().TmdbId;

            pagedItems.NextCursor = nextCursor;

            return pagedItems;
        }
    }
}
