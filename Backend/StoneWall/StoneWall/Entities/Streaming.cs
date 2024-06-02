using System.ComponentModel.DataAnnotations;

namespace StoneWall.Entities
{
    public class Streaming
    {
        [Key]
        [MaxLength(255)]
        public string Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [Required]
        [MaxLength(255)]
        public string HomePage { get; set; }
        public List<ItemCatalogStreaming>? Items { get; set; }
        public List<Addon>? Addons { get; set; }
    }
}
