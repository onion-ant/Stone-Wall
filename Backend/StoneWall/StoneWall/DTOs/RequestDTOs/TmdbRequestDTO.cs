namespace StoneWall.DTOs.RequestDTOs
{
    public record TmdbRequestDTO
    (
        string sizeParams = "original",
        string language = "pt-BR"
    );
}
