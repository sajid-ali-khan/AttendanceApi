using System.Diagnostics;
using System.Text;
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
        CreateMap<CourseAssignment, CourseAssignmentOutputDto>()
            .ForMember(dest => dest.BranchShortName,
                opt => opt.MapFrom(ca => ca.Course.StudentBatch.OfferedProgram.Branch.ShortName))
            .ForMember(dest => dest.SubjectShortName, opt => opt.MapFrom(ca => ca.Course.Subject.ShortName))
            .ForMember(dest => dest.SubjectLongName, opt => opt.MapFrom(ca => ca.Course.Subject.FullName))
            .ForMember(dest => dest.Semester, opt => opt.MapFrom(ca => ca.Course.StudentBatch.Semester))
            .ForMember(dest => dest.Section, opt => opt.MapFrom(ca => ca.Course.StudentBatch.Section))
            .ForMember(dest => dest.FacultyName, opt => opt.MapFrom(ca => ca.Faculty.Name))
            .ForMember(dest => dest.SubjectType, opt => opt.MapFrom(ca => ca.Course.Subject.SubjectType.ToString()));

        CreateMap<Session, SessionOutputDto>()
            .ForMember(dest => dest.CourseName,
                opt => opt.MapFrom(src => FormClassName(src)))
            .ForMember(dest => dest.FacultyName,
                opt => opt.MapFrom(src => src.Faculty!.Name))
            .ForMember(dest => dest.TotalStudentCount,
                opt => opt.MapFrom(src => src.NumPresent + src.NumAbsent))
            .ForMember(dest => dest.PresentStudentCount,
                opt => opt.MapFrom(src => src.NumPresent))
            .ForMember(dest => dest.UpdatedAt,
                opt => opt.MapFrom(src => src.UpdatedDate.ToString("dd/MM/yyyy hh:mm tt")));

        CreateMap<CourseAssignment, FacultyInCourseAssignmentDto>()
            .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.Faculty!.Name))
            .ForMember(dest => dest.Code,
                opt => opt.MapFrom(src => src.Faculty!.Code));
        
        CreateMap<Course, CourseAssignmentForAClassDto>()
            .ForMember(dest => dest.SubjectShortName,
                opt => opt.MapFrom(src => src.Subject.ShortName))
            .ForMember(dest => dest.FacultyAssignments,
                opt => opt.MapFrom(src => src.CourseAssignments));
    }

    private static string FormClassName(Session session)
    {
        //4th Sem CST - A
        var sem = session.Course!.StudentBatch.Semester;
        var opName = session.Course.StudentBatch.OfferedProgram.Branch.ShortName;
        var section = session.Course.StudentBatch.Section;

        var semStr = sem switch
        {
            1 => sem + "st",
            2 => sem + "nd",
            3 => sem + "rd",
            _ => sem + "th"
        };

        var courseName = $"{semStr} Sem {opName} - {section}";
        return courseName;
    }
}