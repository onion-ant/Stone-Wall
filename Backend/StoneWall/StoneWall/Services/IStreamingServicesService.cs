using StoneWall.Data;
using StoneWall.Entities;
using StoneWall.Helpers;

namespace StoneWall.Services
{
    public interface IStreamingServicesService
    {
        public Task<List<StreamingServices>> GetStreamingsAsync();
        public Task<ItemStreamingPaginationHelper> GetItemsAsync(string streamingId, int pageNumber);
    }
}
