using System.ComponentModel.DataAnnotations.Schema;

namespace StoneWall.Helpers
{
    public class TmdbJsonHelper
    {
        public string? poster_path { get; set; }
        public string? backdrop_path { get; set; }
        public string? overview { get; set; }
        public string? release_date { get; set; }
        public string? first_air_date { get; set; }
        public string? last_air_date { get; set; }
        public int number_of_seasons { get; set; }
    }
}
