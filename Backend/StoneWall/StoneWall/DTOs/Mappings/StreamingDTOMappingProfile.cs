using AutoMapper;
using StoneWall.Entities;

namespace StoneWall.DTOs.Mappings
{
    public class StreamingDTOMappingProfile : Profile
    {
        public StreamingDTOMappingProfile()
        {
            CreateMap<Streaming,StreamingDTO>();
            CreateMap<Addon, AddonDTO>();
            CreateMap<ItemCatalogStreaming, ItemStreamingDTO>();
        }
    }
}
