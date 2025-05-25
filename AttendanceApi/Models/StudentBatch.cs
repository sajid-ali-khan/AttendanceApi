using System.ComponentModel.DataAnnotations;

namespace AttendanceApi.Models;

public class StudentBatch
{
    public int Id { get; set; }
    public int OfferedProgramId { get; set; }
    public int Semester { get; set; }
    [MaxLength(3)]
    public required string Section { get; set; }
    
    public required OfferedProgram OfferedProgram { get; set; }
    public ICollection<Student> Students { get; set; } = new List<Student>();
    public ICollection<Course> Courses { get; set; } = new List<Course>(); 
}