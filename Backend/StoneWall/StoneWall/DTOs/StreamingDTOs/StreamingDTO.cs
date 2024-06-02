using StoneWall.Entities;
using System.ComponentModel.DataAnnotations;

namespace StoneWall.DTOs.StreamingDTOs
{
    public record StreamingDTO(
        string Id,
        string Name,
        string HomePage
    );
}
