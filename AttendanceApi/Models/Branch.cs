using System.ComponentModel.DataAnnotations;

namespace AttendanceApi.Models;

public class Branch
{
    public int Id { get; set; }
    [MaxLength(10)]
    public required string ShortName { get; set; }
    [MaxLength(50)]
    public required string FullName { get; set; }
}