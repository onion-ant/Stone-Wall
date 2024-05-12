using StoneWall.Entities;
using StoneWall.Entities.Enums;
using StoneWall.Helpers;
using StoneWall.Pagination;

namespace StoneWall.Services
{
    public interface IItemsService
    {
        public Task<PagedList<Item>> GetItemsAsync(int offset, int pageNumber,ItemParameters itemParams);
        public Task<Item> GetDetailsAsync(int tmdbId);
    }
}
