using StoneWall.Entities;
using StoneWall.Entities.Enums;
using StoneWall.Helpers;

namespace StoneWall.Services
{
    public interface IItemsService
    {
        public Task<ItemPaginationHelper> GetItemsAsync(int pageNumber, int offset, int genreId, int atLeast, ItemType? itemType);
        public Task<Item> GetDetailsAsync(int tmdbId);
    }
}
