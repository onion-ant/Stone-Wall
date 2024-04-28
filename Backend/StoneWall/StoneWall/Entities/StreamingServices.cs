using System.ComponentModel.DataAnnotations;

namespace StoneWall.Entities
{
    public class StreamingServices
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
        public List<Addon>? Addons { get; set; }
    }
}
