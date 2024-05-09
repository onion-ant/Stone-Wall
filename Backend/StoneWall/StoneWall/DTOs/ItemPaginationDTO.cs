namespace StoneWall.DTOs
{
    public record ItemPaginationDTO(
        IEnumerable<ItemDTO> Items,
        int LastPage
    );
}
