using StoneWall.Entities.Enums;

namespace StoneWall.Pagination
{
    public record ItemParameters
    (
        string? name,
        string? genreId,
        ItemType? itemType,
        int? atLeast = 0
    );
}
