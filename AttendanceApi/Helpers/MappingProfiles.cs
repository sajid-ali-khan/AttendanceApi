using AttendanceApi.Dtos;
using AttendanceApi.Models;
using AttendanceApi.Models.Enums;
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
        CreateMap<StudentBatch, StudentBatchOutputDto>();
        CreateMap<Course, CourseOutputDto>()
            .ForMember(dest => dest.SubjectNameShort,
                opt => opt.MapFrom(src => src.Subject.ShortName))
            .ForMember(dest => dest.SubjectNameLong,
                opt => opt.MapFrom(src => src.Subject.FullName))
            .ForMember(dest => dest.SubjectType,
                opt => opt.MapFrom(src => src.Subject.SubjectType.ToString()));
        CreateMap<Faculty, FacultyOutputDto>();
    }
}