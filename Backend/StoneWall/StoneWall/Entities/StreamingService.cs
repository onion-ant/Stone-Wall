using System.ComponentModel.DataAnnotations;

namespace StoneWall.Entities
{
    public class StreamingService
    {
        [Key]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? HomePage { get; set; }
        public List<ItemStreaming>? Items { get; set; }
    }
}
