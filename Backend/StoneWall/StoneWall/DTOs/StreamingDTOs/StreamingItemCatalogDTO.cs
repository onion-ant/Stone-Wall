using StoneWall.Entities.Enums;
using StoneWall.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using StoneWall.DTOs.ItemCatalogDTOs;

namespace StoneWall.DTOs.StreamingDTOs
{
    public record StreamingItemCatalogDTO(
        ItemCatalogDTO Item,
        double? Price,
        StreamingType Type
    );
}
