using AttendanceApi.Models.Enums;

namespace AttendanceApi.Models;

public class Subject
{
    public int Id { get; set; }
    public string ShortName { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public SubjectType SubjectType { get; set; }
}