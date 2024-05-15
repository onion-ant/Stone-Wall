using StoneWall.Entities;
using StoneWall.Entities.Enums;

namespace StoneWall.DTOs
{
    public record ItemDTO(
        int TmdbId,
        string Title,
        string OriginalTitle,
        double Popularity,
        ItemType Type,
        List<ItemStreamingDTO> Streamings,
        List<GenreDTO> Genres,
        string? Overview,
        string? PosterPath,
        string? BackdropPath,
        int ReleaseYear,
        int LastAirYear,
        int SeasonsCount
    );
}
