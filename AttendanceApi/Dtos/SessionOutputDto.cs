namespace AttendanceApi.Dtos;

public class SessionOutputDto
{
    public int Id { get; set; }
    public string CourseName { get; set; } = string.Empty;
    public string FacultyName { get; set; } = string.Empty;
    public string UpdatedAt { get; set; } = string.Empty;
    public int TotalStudentCount { get; set; }
    public int PresentStudentCount { get; set; }
}