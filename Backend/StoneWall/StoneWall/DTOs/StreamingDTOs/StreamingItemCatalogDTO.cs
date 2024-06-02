using StoneWall.Entities.Enums;
using StoneWall.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using StoneWall.DTOs.ItemCatalogDTOs;
using System.Text.Json.Serialization;

namespace StoneWall.DTOs.StreamingDTOs
{
    public record StreamingItemCatalogDTO
    {
        public ItemCatalogDTO Item { get; set; }
        public double? Price { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public bool ExpiresSoon { get; set; }
        public StreamingType Type { get; set; }
    }
}
