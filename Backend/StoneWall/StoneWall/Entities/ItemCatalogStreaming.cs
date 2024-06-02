using Microsoft.EntityFrameworkCore;
using StoneWall.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneWall.Entities
{
    [PrimaryKey(nameof(ItemCatalogTmdbId), nameof(StreamingId))]
    public class ItemCatalogStreaming
    {
        [ForeignKey(nameof(Entities.ItemCatalog))]
        public string ItemCatalogTmdbId { get; set; }
        public ItemCatalog Item { get; set; }
        [ForeignKey(nameof(Entities.Streaming))]
        public string StreamingId { get; set; }
        public Streaming Streaming { get; set; }
        public bool expiresSoon { get; set; }
        public double? Price { get; set; }
        [Required]
        public StreamingType Type { get; set; }
        [Required]
        [MaxLength(255)]
        public string Link { get; set; }
    }
}
