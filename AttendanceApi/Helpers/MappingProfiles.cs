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
        CreateMap<Faculty, FacultyOutputDtoSingle>()
            .ForMember(dest => dest.Role,
                opt => opt.MapFrom(src => src.Role.ToString()));

        CreateMap<CourseAssignmentCreateDto, CourseAssignment>();
        CreateMap<CourseAssignment, CourseAssignmentOutputDtoSingle>()
            .ForMember(dest => dest.BranchShortName,
                opt => opt.MapFrom(ca => ca.Course.StudentBatch.OfferedProgram.Branch.ShortName))
            .ForMember(dest => dest.SubjectShortName, opt => opt.MapFrom(ca => ca.Course.Subject.ShortName))
            .ForMember(dest => dest.SubjectLongName, opt => opt.MapFrom(ca => ca.Course.Subject.FullName))
            .ForMember(dest => dest.Semester, opt => opt.MapFrom(ca => ca.Course.StudentBatch.Semester))
            .ForMember(dest => dest.Section, opt => opt.MapFrom(ca => ca.Course.StudentBatch.Section))
            .ForMember(dest => dest.FacultyName, opt => opt.MapFrom(ca => ca.Faculty.Name))
            .ForMember(dest => dest.SubjectType, opt => opt.MapFrom(ca => ca.Course.Subject.SubjectType.ToString()));
    }
}