using Microsoft.EntityFrameworkCore;
using PopulateDatabase.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneWall.Entities
{
    [PrimaryKey(nameof(TmdbId), nameof(StreamingId))]
    public class ItemStreaming
    {
        [ForeignKey("Item")]
        public int? TmdbId { get; set; }
        public Item? Item { get; set; }
        [ForeignKey("StreamingService")]
        public string? StreamingId { get; set; }
        public StreamingService? Streaming { get; set; }
        public StreamingType Type { get; set; }
        public string? Link { get; set; }
    }
}
