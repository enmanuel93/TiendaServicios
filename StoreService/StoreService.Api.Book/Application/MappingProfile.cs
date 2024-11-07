using AutoMapper;
using StoreService.Api.Book.DTos;
using StoreService.Api.Book.Models;

namespace StoreService.Api.Book.Application
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<LibreriaMaterial, LibreriaMaterialDto>();
        }
    }
}
