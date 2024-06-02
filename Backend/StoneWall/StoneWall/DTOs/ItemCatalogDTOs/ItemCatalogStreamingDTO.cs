using StoneWall.Entities.Enums;
using StoneWall.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using StoneWall.DTOs.StreamingDTOs;
using System.Text.Json.Serialization;

namespace StoneWall.DTOs.ItemCatalogDTOs
{
    public class ItemCatalogStreamingDTO
    {
        public string StreamingId { get; set; }
        public double? Price { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public bool ExpiresSoon { get; set; }
        public string Link {  get; set; }
        public StreamingType Type { get; set; }
    }
}
