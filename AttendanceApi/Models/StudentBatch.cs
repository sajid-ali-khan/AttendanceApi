namespace AttendanceApi.Models;

public class StudentBatch
{
    public int Id { get; set; }
    public int ProgramId { get; set; }
    public int Semester { get; set; }
    public required string Section { get; set; }
    
    public required Program Program { get; set; }
    public ICollection<Student> Students { get; set; } = new List<Student>();
    public ICollection<Course> Courses { get; set; } = new List<Course>(); 
}