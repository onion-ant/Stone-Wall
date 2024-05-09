using StoneWall.Entities;
using System.ComponentModel.DataAnnotations;

namespace StoneWall.DTOs
{
    public record StreamingDTO(
        string Id,
        string Name,
        string HomePage
    );
}
