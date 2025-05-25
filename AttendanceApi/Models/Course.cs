namespace AttendanceApi.Models;

public class Course
{
    public int Id { get; set; }
    public int StudentBatchId { get; set; }
    public int SubjectId { get; set; }
    
    public required StudentBatch StudentBatch { get; set; }
    public required Subject Subject { get; set; }
    
    public ICollection<CourseAssignment> CourseAssignments { get; set; } = new List<CourseAssignment>();
    public ICollection<Session> Sessions { get; set; } = new List<Session>();
}