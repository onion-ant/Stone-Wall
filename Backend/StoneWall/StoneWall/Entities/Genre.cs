using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneWall.Entities
{
    public class Genre
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public List<ItemCatalog>? Items { get; set; }
    }
}