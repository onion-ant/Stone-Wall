﻿using StoneWall.Entities.Enums;
using StoneWall.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StoneWall.DTOs
{
    public record ItemStreamingDTO(
        StreamingType Type,
        string Link
    );
}
