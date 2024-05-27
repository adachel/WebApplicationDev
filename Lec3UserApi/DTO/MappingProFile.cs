using AutoMapper;
using Lec3UserApi.DB;

namespace Lec3UserApi.DTO
{
    public class MappingProFile : Profile // для autoMapper
    {
        public MappingProFile() 
        {
            CreateMap<UserDTO, User>()
                .ForMember(dest => dest.Id, opts => opts.Ignore())
                .ForMember(dest => dest.Email, opts => opts.MapFrom(y => y.Email))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(y => y.Name))
                .ForMember(dest => dest.Surname, opts => opts.MapFrom(y => y.FamilyName))
                .ForMember(dest => dest.Registered, opts => opts.Ignore())
                .ForMember(dest => dest.Active, opts => opts.Ignore()).ReverseMap();
        }
    }
}
