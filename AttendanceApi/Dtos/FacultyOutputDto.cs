using AttendanceApi.Models.Enums;

namespace AttendanceApi.Dtos;

public class FacultyOutputDto
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}