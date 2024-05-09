using StoneWall.Data;
using StoneWall.Entities;
using StoneWall.Entities.Enums;
using StoneWall.Helpers;

namespace StoneWall.Services
{
    public interface IStreamingServicesService
    {
        public Task<IEnumerable<StreamingServices>> GetStreamingsAsync();
        public Task<IEnumerable<Addon>> GetAddonsAsync(string streamingId);
        public Task<ItemStreamingPaginationHelper> GetItemsAsync(string streamingId, int pageNumber,int offset,int genreId, ItemType? itemType, StreamingType? streamingType);
        public Task<ItemStreamingPaginationHelper> CompareStreamings(string streamingExclusivo, string strimingExcluido, int pageNumber, int offset, int genreId, ItemType? itemType, StreamingType? streamingType);
    }
}
