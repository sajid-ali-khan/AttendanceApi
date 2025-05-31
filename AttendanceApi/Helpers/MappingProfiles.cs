using AttendanceApi.Dtos;
using AttendanceApi.Models;
using AutoMapper;

namespace AttendanceApi.Helpers;

public class MappingProfiles: Profile
{
    public MappingProfiles()
    {
        CreateMap<Scheme, SchemeOutputDto>();
    }
}