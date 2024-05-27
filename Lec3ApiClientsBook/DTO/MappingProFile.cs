using AutoMapper;
using Lec3ApiClientsBook.DB;

namespace Lec3ApiClientsBook.DTO
{
    public class MappingProFile : Profile
    {
        public MappingProFile() 
        {
            CreateMap<ClientBookDTO, ClientBook>().ReverseMap();
        }
    }
}
