using Newtonsoft.Json;
using StoneWall.Data;
using StoneWall.DTOs.ExternalApiDTOs;
using StoneWall.DTOs.RequestDTOs;
using StoneWall.Entities;
using StoneWall.Entities.Enums;
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
        public async Task GetItemAsync(ItemCatalog Item,TmdbRequestDTO tmdbParams)
        {
            try
            {
                var request = RequestBuilder(Item.TmdbId, tmdbParams.language);
                var response = await _client.SendAsync(request);
                if( !response.IsSuccessStatusCode )
                {
                    throw new ExternalApiException("Invalid item");
                }
                string? body = await response.Content.ReadAsStringAsync();
                TmdbDTO? itemJsonHelper = JsonConvert.DeserializeObject<TmdbDTO>(body);
                Item.Overview = itemJsonHelper!.overview;
                Item.PosterPath = $"https://image.tmdb.org/t/p/{tmdbParams.sizeParams}/" + itemJsonHelper.poster_path;
                Item.BackdropPath = $"https://image.tmdb.org/t/p/{tmdbParams.sizeParams}/" + itemJsonHelper.backdrop_path;
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
        private HttpRequestMessage RequestBuilder(string? TmdbId, string language)
        {
            return new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://api.themoviedb.org/3/{TmdbId}?language={language}"),
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
