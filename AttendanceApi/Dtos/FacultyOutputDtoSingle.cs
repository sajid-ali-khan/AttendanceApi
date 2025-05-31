using AttendanceApi.Models.Enums;

namespace AttendanceApi.Dtos;

public class FacultyOutputDtoSingle
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public FacultyRole Role { get; set; }
}