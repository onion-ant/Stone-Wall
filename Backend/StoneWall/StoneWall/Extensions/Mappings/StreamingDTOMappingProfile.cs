﻿using StoneWall.DTOs;
using StoneWall.DTOs.ItemCatalogDTOs;
using StoneWall.Entities;
using StoneWall.DTOs.StreamingDTOs;
using StoneWall.Pagination;

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
        public static StreamingItemCatalogPaginationDTO? ToStreamingItemPaginationDTO(this CursorList<ItemCatalogStreaming> items)
        {
            if (items == null)
            {
                return null;
            }
            return new StreamingItemCatalogPaginationDTO
            {
                Items = items.Select(item => item.ToStreamingItemCatalogDTO()).ToList(),
                NextCursor = items.NextCursor,
                HasNext = items.HasNext
            };
        }
        public static StreamingItemCatalogDTO? ToStreamingItemCatalogDTO(this ItemCatalogStreaming itemStreaming)
        {
            if(itemStreaming == null)
            {
                return null;
            }
            return new StreamingItemCatalogDTO
            {
                Item = itemStreaming.Item.ToItemDTO(),
                ExpiresSoon = itemStreaming.expiresSoon,
                Price = itemStreaming.Price != null ? itemStreaming.Price : null,
                Type = itemStreaming.Type,
                Link = itemStreaming.Link
            };
        }
    }
}
