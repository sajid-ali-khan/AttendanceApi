using System.ComponentModel.DataAnnotations;
using AttendanceApi.Models.Enums;

namespace AttendanceApi.Models;

public class Faculty
{
    public int Id { get; set; }
    [MaxLength(5)]
    public required string Code { get; set; }
    [MaxLength(50)]
    public string? Name { get; set; }
    public FacultyRole Role { get; set; }
    public required string PasswordHash { get; set; }
    
    public ICollection<CourseAssignment> CourseAssignments { get; set; } = new List<CourseAssignment>();
    public ICollection<Session> Sessions { get; set; } = new List<Session>();
}