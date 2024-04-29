using StoneWall.Entities;

namespace StoneWall.Services
{
    public interface ITmdbService
    {
        public Task GetItemAsync(Item Items,string language="pt-BR");
    }
}
