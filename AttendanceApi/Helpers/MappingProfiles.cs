using AttendanceApi.Dtos;
using AttendanceApi.Models;
using AutoMapper;

namespace AttendanceApi.Helpers;

public class MappingProfiles: Profile
{
    public MappingProfiles()
    {
        CreateMap<Scheme, SchemeOutputDto>();
        CreateMap<OfferedProgram, OfferedProgramsOutputDto>()
            .ForMember(dest => dest.ShortName, opt => opt.MapFrom(src => src.Branch.ShortName))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Branch.FullName));
    }
}