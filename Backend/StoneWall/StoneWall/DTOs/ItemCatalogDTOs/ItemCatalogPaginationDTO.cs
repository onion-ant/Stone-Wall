namespace StoneWall.DTOs.ItemCatalogDTOs
{
    public class ItemCatalogPaginationDTO
    {
        public List<ItemCatalogDTO> Items { get; set; }
        public string NextCursor { get; set; }
        public bool HasNext { get; set; }
    }
}
