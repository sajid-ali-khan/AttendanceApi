using AttendanceApi.Models.Enums;

namespace AttendanceApi.Models;

public class Faculty
{
    public int Id { get; set; }
    public required string Code { get; set; }
    public string Name { get; set; } = string.Empty;
    public FacultyRole Role { get; set; }
    public required string PasswordHash { get; set; }
    
    public ICollection<CourseAssignment> CourseAssignments { get; set; } = new List<CourseAssignment>();
    public ICollection<Session> Sessions { get; set; } = new List<Session>();
}