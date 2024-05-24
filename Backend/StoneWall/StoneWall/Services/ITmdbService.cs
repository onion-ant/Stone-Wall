using StoneWall.Entities;
using StoneWall.Pagination;

namespace StoneWall.Services
{
    public interface ITmdbService
    {
        public Task GetItemAsync(ItemCatalog Item, TmdbParameters tmdbParams);
    }
}
