using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneWall.Entities
{
    [PrimaryKey(nameof(TmdbId), nameof(GenreId))]
    public class ItemGenre
    {
        [ForeignKey(nameof(Entities.Item))]
        public int? TmdbId { get; set; }
        public Item? Item { get; set; }
        [ForeignKey(nameof(Entities.Genre))]
        public int GenreId { get; set; }
        public Genre? Genre { get; set; }
    }
}