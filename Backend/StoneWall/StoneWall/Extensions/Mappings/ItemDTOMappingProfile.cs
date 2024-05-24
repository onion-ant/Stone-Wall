using StoneWall.DTOs;
using StoneWall.Entities;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace StoneWall.Extensions.Mappings
{
    public static class ItemDTOMappingProfile
    {
        public static ItemDTO ToItemDTO(this ItemCatalog itemCatalog)
        {
            return new ItemDTO
            (
                itemCatalog.TmdbId,
                itemCatalog.Title,
                itemCatalog.OriginalTitle,
                itemCatalog.Rating,
                itemCatalog.Type,
                itemCatalog.Streamings.Select(s => s.ToItemStreamingDTO()).ToList(),
                itemCatalog.Genres.Select(g => g.ToGenreDTO()).ToList(),
                itemCatalog.Overview,
                itemCatalog.PosterPath,
                itemCatalog.BackdropPath,
                itemCatalog.ReleaseYear,
                itemCatalog.LastAirYear,
                itemCatalog.SeasonsCount
            );
        }
        public static GenreDTO ToGenreDTO(this Genre genre)
        {
            return new GenreDTO
            (
                genre.Id,
                genre.Name
            );
        }
    }
}
