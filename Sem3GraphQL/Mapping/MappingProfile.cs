using AutoMapper;
using Sem3GraphQL.DTO;
using Sem3GraphQL.Models;

namespace Sem3GraphQL.Mapping
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
