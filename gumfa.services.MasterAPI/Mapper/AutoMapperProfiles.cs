using AutoMapper;
using gumfa.services.MasterAPI.Models;
using gumfa.services.MasterAPI.Models.DTO;

namespace gumfa.services.MasterAPI.Mapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Product, ProductListDto>().ReverseMap();
            CreateMap<Product, ProductAddDto>().ReverseMap();
            CreateMap<Product, ProductUpdateDto >().ReverseMap();
        }
    }
}
