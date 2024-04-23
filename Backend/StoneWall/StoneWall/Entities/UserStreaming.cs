using Microsoft.EntityFrameworkCore;
using PopulateDatabase.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneWall.Entities
{
    [PrimaryKey(nameof(UserEmail),nameof(StreamingId))]
    public class UserStreaming
    {
        [ForeignKey("User")]
        public string? UserEmail { get; set; }
        [ForeignKey("StreamingService")]
        public string? StreamingId { get; set; }
        [Range(0, 5)]
        public int Rating { get; set; }
        public string? Review { get; set; }
        [Required]
        public StreamingType? Plan { get; set; }

    }
}
