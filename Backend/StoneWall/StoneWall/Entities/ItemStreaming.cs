﻿using Microsoft.EntityFrameworkCore;
using StoneWall.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneWall.Entities
{
    [PrimaryKey(nameof(TmdbId), nameof(StreamingId))]
    public class ItemStreaming
    {
        [ForeignKey(nameof(Entities.Item))]
        public int? TmdbId { get; set; }
        public Item? Item { get; set; }
        [ForeignKey(nameof(StreamingServices))]
        public string? StreamingId { get; set; }
        public StreamingServices? Streaming { get; set; }
        [Required]
        public StreamingType Type { get; set; }
        [Required]
        [MaxLength(255)]
        public string? Link { get; set; }
    }
}
