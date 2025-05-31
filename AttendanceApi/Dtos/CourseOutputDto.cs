using AttendanceApi.Models.Enums;

namespace AttendanceApi.Dtos;

public class CourseOutputDto
{
    public int Id { get; set; }
    public string SubjectNameShort { get; set; } = string.Empty;
    public string SubjectNameLong { get; set; } = string.Empty;
    public SubjectType SubjectType { get; set; }
}