using AutoMapper;
using Lec3LibraryApi.DB;

namespace Lec3LibraryApi.DTO.Map
{
    public class MappingProFile : Profile
    {
        public MappingProFile() 
        {
            CreateMap<AuthorDTO, Author>().ReverseMap(); // Id и Name одинаковые, значит просто ReverseMap
            CreateMap<BookDTO, Book>().ReverseMap();
        }
    }
}
