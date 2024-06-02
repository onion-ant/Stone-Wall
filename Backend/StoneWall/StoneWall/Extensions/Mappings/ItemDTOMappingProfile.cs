using StoneWall.DTOs;
using StoneWall.DTOs.ItemCatalogDTOs;
using StoneWall.Entities;
using StoneWall.Pagination;
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
        public static ItemCatalogPaginationDTO? ToItemPaginationDTO(this CursorList<ItemCatalog> items)
        {
            if (items == null)
            {
                return null;
            }
            return new ItemCatalogPaginationDTO
            {
                Items = items.Select(item => item.ToItemDTO()).ToList(),
                NextCursor = items.NextCursor,
                HasNext = items.HasNext
            };
        }
        public static ItemCatalogStreamingDTO? ToItemCatalogStreamingDTO(this ItemCatalogStreaming itemStreaming)
        {
            if(itemStreaming == null)
            {
                return null;
            }
            return new ItemCatalogStreamingDTO
            {
                StreamingId = itemStreaming.StreamingId,
                Price = itemStreaming.Price,
                ExpiresSoon = itemStreaming.expiresSoon,
                Type = itemStreaming.Type,
                Link = itemStreaming.Link
            };
        }
    }
}
