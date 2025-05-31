namespace AttendanceApi.Dtos;

public class CourseAssignmentCreateDto
{
    public int CourseId { get; set; }
    public int FacultyId { get; set; }
    public string? AssignedRole { get; set; }
}