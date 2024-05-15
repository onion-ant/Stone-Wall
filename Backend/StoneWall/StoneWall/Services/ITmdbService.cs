using StoneWall.Entities;
using StoneWall.Pagination;

namespace StoneWall.Services
{
    public interface ITmdbService
    {
        public Task GetItemAsync(Item Item, TmdbParameters tmdbParams);
    }
}
