using StoneWall.Entities.Enums;
using StoneWall.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using StoneWall.DTOs.StreamingDTOs;

namespace StoneWall.DTOs.ItemCatalogDTOs
{
    public record ItemCatalogStreamingDTO(
        string StreamingId,
        double? Price,
        StreamingType Type
    );
}
