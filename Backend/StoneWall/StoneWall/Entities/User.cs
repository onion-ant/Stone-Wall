using System.ComponentModel.DataAnnotations;

namespace StoneWall.Entities
{
    public class User
    {
        [Key]
        [RegularExpression(".+\\@.+\\..+")]
        [MaxLength(255)]
        public string? Email { get; set; }
        [Required]
        [MaxLength(60)]
        public string? Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string? Password { get; set; }
    }
}
