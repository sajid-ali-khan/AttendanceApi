using System.ComponentModel.DataAnnotations;
using AttendanceApi.Models.Enums;

namespace AttendanceApi.Models;

public class Subject
{
    public int Id { get; set; }
    [MaxLength(20)]
    public string ShortName { get; set; } = string.Empty;
    [MaxLength(100)]
    public string FullName { get; set; } = string.Empty;
    public SubjectType SubjectType { get; set; }
    
    public ICollection<Course> Courses { get; set; } = new List<Course>();
}