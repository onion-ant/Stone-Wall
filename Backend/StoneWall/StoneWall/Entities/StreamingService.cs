using System.ComponentModel.DataAnnotations;

namespace StoneWall.Entities
{
    public class StreamingService
    {
        [Key]
        [MaxLength(255)]
        public string? Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string? Name { get; set; }
        [Required]
        [MaxLength(255)]
        public string? HomePage { get; set; }
        public List<ItemStreaming>? Items { get; set; }
    }
}
