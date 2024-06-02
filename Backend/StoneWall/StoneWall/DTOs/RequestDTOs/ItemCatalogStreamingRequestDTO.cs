using StoneWall.Entities.Enums;

namespace StoneWall.DTOs.RequestDTOs
{
    public record ItemCatalogStreamingRequestDTO
    (
        string? name,
        string? genreId,
        ItemType? itemType,
        int atLeast = 0
    );
}
