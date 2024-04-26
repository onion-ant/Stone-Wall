using StoneWall.Data;
using StoneWall.Entities;

namespace StoneWall.Services
{
    public interface IStreamingServicesService
    {
        public Task<List<StreamingServices>> GetStreamingsAsync();
        public Task<List<ItemStreaming>> GetItemsAsync(string streamingId);
    }
}
