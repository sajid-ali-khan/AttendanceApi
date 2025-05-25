using System.ComponentModel.DataAnnotations;

namespace AttendanceApi.Models;

public class Scheme
{
    public int Id { get; set; }
    [MaxLength(5)]
    public required string Name { get; set; }
    
    public ICollection<OfferedProgram> OfferedPrograms { get; set; } = new List<OfferedProgram>();
}