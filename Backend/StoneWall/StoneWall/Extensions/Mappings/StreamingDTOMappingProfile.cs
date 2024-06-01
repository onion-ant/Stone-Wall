using StoneWall.DTOs;
using StoneWall.DTOs.ItemCatalogDTOs;
using StoneWall.Entities;
using StoneWall.DTOs.StreamingDTOs;

namespace StoneWall.Extensions.Mappings
{
    public static class StreamingDTOMappingProfile
    {
        public static StreamingDTO? ToStreamingDTO(this Streaming streaming)
        {
            if(streaming == null)
            {
                return null;
            }
            return new StreamingDTO
            (
                streaming.Id,
                streaming.Name,
                streaming.HomePage
            );
        }
        public static AddonDTO? ToAddonDTO(this Addon addon)
        {
            if(addon == null)
            {
                return null;
            }
            return new AddonDTO
            (
                addon.Id,
                addon.Name,
                addon.HomePage
            );
        }
        public static StreamingItemCatalogDTO? ToStreamingItemCatalogDTO(this ItemCatalogStreaming itemStreaming)
        {
            if(itemStreaming == null)
            {
                return null;
            }
            return new StreamingItemCatalogDTO
            (
                itemStreaming.Item.ToItemDTO(),
                itemStreaming.Price != null ? itemStreaming.Price : null,
                itemStreaming.Type
            );
        }
    }
}
