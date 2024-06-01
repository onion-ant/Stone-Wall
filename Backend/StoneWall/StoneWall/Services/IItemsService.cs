using StoneWall.Entities;
using StoneWall.Entities.Enums;
using StoneWall.DTOs.ExternalApiDTOs;
using StoneWall.Pagination;
using X.PagedList;

namespace StoneWall.Services
{
    public interface IItemsService
    {
        public Task<CursorList<ItemCatalog>> GetItemsAsync(int limit, string? cursor,ItemParameters itemParams);
        public Task<ItemCatalog> GetDetailsAsync(string tmdbId);
    }
}
