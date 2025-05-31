using System.ComponentModel.DataAnnotations;

namespace AttendanceApi.Dtos;

public class StudentBatchOutputDto
{
    public int Id { get; set; }
    public int Semester { get; set; }
    [MaxLength(2)] public string Section { get; set; } = string.Empty;
}