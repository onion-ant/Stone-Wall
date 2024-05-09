using StoneWall.Entities;

namespace StoneWall.DTOs
{
    public record ItemStreamingPaginationDTO(
        IEnumerable<ItemStreamingDTO> Items,
        int LastPage
    );
}
