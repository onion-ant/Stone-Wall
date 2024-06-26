﻿using StoneWall.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneWall.Entities
{
    public class ItemCatalog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string TmdbId { get; set; }
        [MaxLength(255)]
        public string Title { get; set; }
        [MaxLength(255)]
        public string OriginalTitle { get; set; }
        public int Rating { get; set; }
        public ItemType Type { get; set; }
        public List<ItemCatalogStreaming>? Streamings { get; set; }
        public List<Genre>? Genres { get; set; }
        [NotMapped]
        public string? Overview { get; set; }
        [NotMapped]
        public string? BackdropPath { get; set; }
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
