using AutoMapper;
using CRUD.Domain.DTOs;
using CRUD.Domain.Entities;

namespace CRUD.Domain.Mapper
{
    public class MapperItem : Profile
    {
        public MapperItem()
        {
            CreateMap<Item, ItemDto>().ReverseMap();
        }
    }
}