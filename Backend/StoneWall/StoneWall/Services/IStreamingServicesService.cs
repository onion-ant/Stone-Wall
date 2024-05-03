using StoneWall.Data;
using StoneWall.Entities;
using StoneWall.Helpers;

namespace StoneWall.Services
{
    public interface IStreamingServicesService
    {
        public Task<List<StreamingServices>> GetStreamingsAsync();
        public Task<List<Addon>> GetAddonsAsync(string streamingId);
        public Task<ItemStreamingPaginationHelper> GetItemsAsync(string streamingId, int pageNumber,int pageSize);
        public Task<ItemStreamingPaginationHelper> CompareStreamings(string streamingExclusivo, string strimingExcluido, int pageNumber, int pageSize);
        public Task<ItemStreamingPaginationHelper> GetItemsByGenreAsync(string streamingId, int pageNumber, int pageSize, int genreId);
    }
}
