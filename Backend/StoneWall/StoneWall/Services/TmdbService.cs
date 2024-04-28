using Newtonsoft.Json;
using StoneWall.Data;
using StoneWall.Entities;
using StoneWall.Entities.Enums;
using StoneWall.Helpers;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace StoneWall.Services
{
    public class TmdbService : ITmdbService
    {
        private readonly string? TmdbKey;
        private readonly HttpClient _client;

        public TmdbService(IConfiguration config, HttpClient client)
        {
            TmdbKey = config["ApiKey"];
            _client = client;
        }
        public async Task GetItemAsync(Item Item,string language)
        {
            HttpRequestMessage request;
            if (Item.Type == ItemType.movie)
            {
                request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://api.themoviedb.org/3/movie/{Item.TmdbId}?language={language}&api_key={TmdbKey}")
                };
                HttpResponseMessage response = null;
                try
                {
                    response = await _client.SendAsync(request);
                }
                catch(Exception ex)
                {
                    await Console.Out.WriteLineAsync(ex.Message);
                }
                string? body = await response.Content.ReadAsStringAsync();
                TmdbJsonHelper? itemJsonHelper = JsonConvert.DeserializeObject<TmdbJsonHelper>(body);
                Item.Overview = itemJsonHelper.overview;
                Item.PosterPath = "https://image.tmdb.org/t/p/original/" + itemJsonHelper.poster_path;
                Item.ReleaseYear = int.Parse(itemJsonHelper.release_date.Split('-')[0]);
            }
            else
            {
                request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://api.themoviedb.org/3/tv/{Item.TmdbId}?language=pt-BR&api_key={TmdbKey}")
                };
                var response = await _client.SendAsync(request);
                string? body = await response.Content.ReadAsStringAsync();
                TmdbJsonHelper? itemJsonHelper = JsonConvert.DeserializeObject<TmdbJsonHelper>(body);
                Item.Overview = itemJsonHelper.overview;
                Item.PosterPath = "https://image.tmdb.org/t/p/original/" + itemJsonHelper.poster_path;
                Item.ReleaseYear = int.Parse(itemJsonHelper.first_air_date.Split('-')[0]);
                Item.LastAirYear = int.Parse(itemJsonHelper.last_air_date.Split('-')[0]);
            }
        }
    }
}
