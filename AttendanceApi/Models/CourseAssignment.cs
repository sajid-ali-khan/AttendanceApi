using AttendanceApi.Models.Enums;

namespace AttendanceApi.Models;

public class CourseAssignment
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public int FacultyId { get; set; }
    public string? AssignedRole { get; set; }
    
    public required Faculty Faculty { get; set; }
    public required Course Course { get; set; }
}