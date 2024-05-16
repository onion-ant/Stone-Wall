using StoneWall.Entities;
using StoneWall.Entities.Enums;
using StoneWall.Helpers;
using StoneWall.Pagination;
using X.PagedList;

namespace StoneWall.Services
{
    public interface IItemsService
    {
        public Task<CursorList<Item>> GetItemsAsync(int limit, string? cursor,ItemParameters itemParams);
        public Task<Item> GetDetailsAsync(int tmdbId);
    }
}
