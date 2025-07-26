using AutoMapper;
using gumfa.services.OrderAPI.Models;
using gumfa.services.OrderAPI.Models.DTO;

namespace gumfa.services.OrderAPI.Mapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Order, OrderListDto>().ReverseMap();
            CreateMap<Order, OrderAddDto>().ReverseMap();
            CreateMap<Order, OrderUpdateDto >().ReverseMap();
        }
    }
}
