using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneWall.Entities
{
    public class Addon
    {
        [MaxLength(255)]
        [Key]
        public string Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string HomePage { get; set; }
        [ForeignKey(nameof(Streaming))]
        public string StreamingId { get; set;}
    }
}
