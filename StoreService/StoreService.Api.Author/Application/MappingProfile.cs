using AutoMapper;
using StoreService.Api.Author.Model;

namespace StoreService.Api.Author.Application
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<AutorLibro, AutorDto>();
        }
    }
}
