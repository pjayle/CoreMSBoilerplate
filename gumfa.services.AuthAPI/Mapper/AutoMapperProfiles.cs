using AutoMapper;
using gumfa.services.AuthAPI.Models;
using gumfa.services.AuthAPI.Models.DTO;

namespace gumfa.services.AuthAPI.Mapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ApplicationUser, UserDto>().ReverseMap();
        }
    }
}
