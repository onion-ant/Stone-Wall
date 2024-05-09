using System.ComponentModel.DataAnnotations;

namespace StoneWall.DTOs
{
    public record AddonDTO(
        string Id,
        string Name,
        string HomePage
    );
}
