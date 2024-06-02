using StoneWall.Entities;
using StoneWall.Entities.Enums;

namespace StoneWall.DTOs.ItemCatalogDTOs
{
    public record ItemCatalogDTO(
        string TmdbId,
        string Title,
        string OriginalTitle,
        int Rating,
        ItemType Type,
        List<ItemCatalogStreamingDTO> Streamings,
        List<GenreDTO> Genres,
        string? Overview,
        string? PosterPath,
        string? BackdropPath,
        int ReleaseYear,
        int LastAirYear,
        int SeasonsCount
    );
}
