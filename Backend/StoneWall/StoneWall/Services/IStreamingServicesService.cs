using StoneWall.Data;
using StoneWall.Entities;
using StoneWall.Entities.Enums;
using StoneWall.DTOs.ExternalApiDTOs;
using StoneWall.Pagination;
using X.PagedList;
using StoneWall.DTOs.RequestDTOs;

namespace StoneWall.Services
{
    public interface IStreamingServicesService
    {
        public Task<IEnumerable<Streaming>> GetStreamingsAsync();
        public Task<IEnumerable<Addon>> GetAddonsAsync(string streamingId);
        public Task<CursorList<ItemCatalogStreaming>> GetItemsAsync(string streamingId, string? cursor, int limit, StreamingType? streamingType, ItemCatalogStreamingRequestDTO itemParams);
        public Task<CursorList<ItemCatalogStreaming>> CompareStreamings(string streamingExclusivo, string strimingExcluido, string? cursor, int limit, StreamingType? streamingType, ItemCatalogStreamingRequestDTO itemParams);
    }
}
