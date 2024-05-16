using StoneWall.Data;
using StoneWall.Entities;
using StoneWall.Entities.Enums;
using StoneWall.Helpers;
using StoneWall.Pagination;
using X.PagedList;

namespace StoneWall.Services
{
    public interface IStreamingServicesService
    {
        public Task<IEnumerable<StreamingServices>> GetStreamingsAsync();
        public Task<IEnumerable<Addon>> GetAddonsAsync(string streamingId);
        public Task<CursorList<ItemStreaming>> GetItemsAsync(string streamingId, string? cursor, int limit, StreamingType? streamingType, ItemParameters itemParams);
        public Task<CursorList<ItemStreaming>> CompareStreamings(string streamingExclusivo, string strimingExcluido, string? cursor, int limit, StreamingType? streamingType, ItemParameters itemParams);
    }
}
