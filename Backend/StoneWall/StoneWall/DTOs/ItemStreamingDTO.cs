using StoneWall.Entities.Enums;
using StoneWall.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StoneWall.DTOs
{
    public record ItemStreamingDTO(
        string StreamingId,
        StreamingType Type,
        string Link
    );
}
