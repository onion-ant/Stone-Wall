using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneWall.Entities
{
    public class Addon
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? HomePage { get; set; }
        [ForeignKey(nameof(StreamingService))]
        public string? StreamingService { get; set;}
    }
}
