using StoneWall.Entities.Enums;

namespace StoneWall.DTOs.RequestDTOs
{
    public record ItemCatalogStreamingRequestDTO
    (
        string? name,
        string? genreId,
        ItemType? itemType,
        double maxPrice,
        int maxRating,
        int minRating = 0,
        double minPrice = 0,
        int atLeast = 0
    );
}
