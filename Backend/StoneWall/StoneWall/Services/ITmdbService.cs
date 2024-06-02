using StoneWall.DTOs.RequestDTOs;
using StoneWall.Entities;

namespace StoneWall.Services
{
    public interface ITmdbService
    {
        public Task GetItemAsync(ItemCatalog Item, TmdbRequestDTO tmdbParams);
    }
}
