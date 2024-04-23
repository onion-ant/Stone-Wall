﻿using StoneWall.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneWall.Entities
{
    public class Item
    {
        [Key]
        public int? TmdbId { get; set; }
        public ItemType? Type { get; set; }
        public List<ItemStreaming>? Streamings { get; set; }
        [NotMapped]
        public string? Title { get; set; }
        [NotMapped]
        public string? Overview { get; set; }
        [NotMapped]
        public string? PosterPath { get; set; }
        [NotMapped]
        public int ReleaseYear { get; set; }
        [NotMapped]
        public int LastAirYear { get; set; }
        [NotMapped]
        public int SeasonsCount { get; set; }
    }
}
