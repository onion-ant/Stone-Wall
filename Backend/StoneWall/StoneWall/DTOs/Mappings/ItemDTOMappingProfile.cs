using AutoMapper;
using StoneWall.Entities;

namespace StoneWall.DTOs.Mappings
{
    public class ItemDTOMappingProfile : Profile
    {
        public ItemDTOMappingProfile()
        {
            CreateMap<ItemCatalog, ItemDTO>();
            CreateMap<Genre, GenreDTO>();
        }
    }
}
