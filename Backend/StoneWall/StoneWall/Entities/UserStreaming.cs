using Microsoft.EntityFrameworkCore;
using StoneWall.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneWall.Entities
{
    [PrimaryKey(nameof(UserEmail),nameof(StreamingId))]
    public class UserStreaming
    {
        [ForeignKey(nameof(User))]
        public string UserEmail { get; set; }
        [ForeignKey(nameof(Streaming))]
        public string StreamingId { get; set; }
        [Range(0, 5)]
        public int? Rating { get; set; }
        public string? Review { get; set; }
        [Required]
        public StreamingType Plan { get; set; }

    }
}
