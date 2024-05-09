using AutoMapper;
using StoneWall.Entities;

namespace StoneWall.DTOs.Mappings
{
    public class StreamingDTOMappingProfile : Profile
    {
        public StreamingDTOMappingProfile()
        {
            CreateMap<StreamingServices,StreamingDTO>();
            CreateMap<Addon, AddonDTO>();
            CreateMap<ItemStreaming, ItemStreamingDTO>();
        }
    }
}
