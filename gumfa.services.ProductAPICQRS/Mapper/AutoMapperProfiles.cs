using AutoMapper;
using gumfa.services.ProductAPICQRS.Models;
using gumfa.services.ProductAPICQRS.Models.DTO;

namespace gumfa.services.ProductAPICQRS.Mapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //CreateMap<Product, ProductListDto>().ReverseMap();
            //CreateMap<Product, ProductAddDto>().ReverseMap();
            //CreateMap<Product, ProductUpdateDto >().ReverseMap();
        }
    }
}
