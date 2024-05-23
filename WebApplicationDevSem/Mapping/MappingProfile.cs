using AutoMapper;
using WebApplicationDevSem.DTO;
using WebApplicationDevSem.Models;

namespace WebApplicationDevSem.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Product, ProductViewModel>().ReverseMap(); // связываем объекты. ReverseMap - мапится и в обратную сторону
            CreateMap<ProductGroup, ProductGroupViewModel>().ReverseMap();

        }
    }
}
