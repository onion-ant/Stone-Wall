using Newtonsoft.Json;
using StoneWall.Data;
using StoneWall.Entities;
using StoneWall.Entities.Enums;
using StoneWall.Helpers;
using StoneWall.Services.Exceptions;
using System.Net.Http.Json;
using System.Text;
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
        public async Task GetItemAsync(Item Item,string language="pt-BR",string sizeParams = "original")
        {
            string url = Item.Type == ItemType.movie ? "https://api.themoviedb.org/3/movie/" : "https://api.themoviedb.org/3/tv/";
            try
            {
                var request = RequestBuilder(Item.TmdbId, language, url);
                var response = await _client.SendAsync(request);
                string? body = await response.Content.ReadAsStringAsync();
                TmdbJsonHelper? itemJsonHelper = JsonConvert.DeserializeObject<TmdbJsonHelper>(body);
                Item.Overview = itemJsonHelper!.overview;
                Item.PosterPath = $"https://image.tmdb.org/t/p/{sizeParams}" + itemJsonHelper.poster_path;
                if (itemJsonHelper.release_date != null)
                {
                    Item.ReleaseYear = ParseYear(itemJsonHelper.release_date);
                }
                else
                {
                    Item.ReleaseYear = ParseYear(itemJsonHelper.first_air_date);
                    Item.LastAirYear = ParseYear(itemJsonHelper.last_air_date);
                    Item.SeasonsCount = itemJsonHelper.number_of_seasons;
                }
            }
            catch (Exception ex)
            {
                throw new ExternalApiException(ex.Message);
            }
        }
        private HttpRequestMessage RequestBuilder(int? TmdbId, string language, string url)
        {
            return new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{url}{TmdbId}?language={language}"),
                Headers =
                {
                    { "accept", "application/json" },
                    { "Authorization", $"Bearer {TmdbKey}" },
                },
            };
        }
        private int ParseYear(string date)
        {
            var yearString = date.Split('-')[0];
            if (int.TryParse(yearString, out int year))
            {
                return year;
            }
            else
            {
                return 0;
            }
        }
    }
}
