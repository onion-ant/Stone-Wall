using StoneWall.DTOs.ItemCatalogDTOs;

namespace StoneWall.DTOs.StreamingDTOs
{
    public class StreamingItemCatalogPaginationDTO
    {
        public List<StreamingItemCatalogDTO> Items { get; set; }
        public string NextCursor { get; set; }
        public bool HasNext { get; set; }
    }
}
