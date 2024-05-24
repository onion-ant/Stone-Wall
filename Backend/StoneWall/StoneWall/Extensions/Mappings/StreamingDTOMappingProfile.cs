using StoneWall.DTOs;
using StoneWall.Entities;

namespace StoneWall.Extensions.Mappings
{
    public static class StreamingDTOMappingProfile
    {
        public static StreamingDTO ToStreamingDTO(this Streaming streaming)
        {
            return new StreamingDTO
            (
                streaming.Id,
                streaming.Name,
                streaming.HomePage
            );
        }
        public static AddonDTO ToAddonDTO(this Addon addon)
        {
            return new AddonDTO
            (
                addon.Id,
                addon.Name,
                addon.HomePage
            );
        }
        public static ItemStreamingDTO ToItemStreamingDTO(this ItemCatalogStreaming itemStreaming)
        {
            return new ItemStreamingDTO
            (
                itemStreaming.Item != null ? itemStreaming.Item.ToItemDTO():null,
                itemStreaming.StreamingId,
                itemStreaming.ItemCatalogTmdbId,
                itemStreaming.Price != null ? itemStreaming.Price : null,
                itemStreaming.Type
            );
        }
    }
}
