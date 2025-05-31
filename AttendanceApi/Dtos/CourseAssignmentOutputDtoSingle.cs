using AttendanceApi.Models.Enums;

namespace AttendanceApi.Dtos;

public class CourseAssignmentOutputDtoSingle
{
    public int Id { get; set; }
    public string BranchShortName { get; set; } = "";
    public int Semester { get; set; }
    public string Section { get; set; } = "";
    public string SubjectShortName { get; set; } = "";
    public string SubjectLongName { get; set; } = "";
    public string FacultyName { get; set; } = "";
    public SubjectType SubjectType { get; set; }
}