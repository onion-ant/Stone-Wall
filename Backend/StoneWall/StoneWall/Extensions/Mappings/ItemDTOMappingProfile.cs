using StoneWall.DTOs;
using StoneWall.DTOs.ItemCatalogDTOs;
using StoneWall.Entities;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace StoneWall.Extensions.Mappings
{
    public static class ItemDTOMappingProfile
    {
        public static ItemCatalogDTO? ToItemDTO(this ItemCatalog itemCatalog)
        {
            if(itemCatalog == null)
            {
                return null;
            }
            return new ItemCatalogDTO
            (
                itemCatalog.TmdbId,
                itemCatalog.Title,
                itemCatalog.OriginalTitle,
                itemCatalog.Rating,
                itemCatalog.Type,
                itemCatalog.Streamings.Select(s => s.ToItemCatalogStreamingDTO()).ToList(),
                itemCatalog.Genres.Select(g => g.ToGenreDTO()).ToList(),
                itemCatalog.Overview,
                itemCatalog.PosterPath,
                itemCatalog.BackdropPath,
                itemCatalog.ReleaseYear,
                itemCatalog.LastAirYear,
                itemCatalog.SeasonsCount
            );
        }
        public static GenreDTO? ToGenreDTO(this Genre genre)
        {
            if(genre == null)
            {
                return null;
            }
            return new GenreDTO
            (
                genre.Id,
                genre.Name
            );
        }
        public static ItemCatalogStreamingDTO? ToItemCatalogStreamingDTO(this ItemCatalogStreaming itemStreaming)
        {
            if(itemStreaming == null)
            {
                return null;
            }
            return new ItemCatalogStreamingDTO
            (
                itemStreaming.StreamingId,
                itemStreaming.Price,
                itemStreaming.Type
            );
        }
    }
}
