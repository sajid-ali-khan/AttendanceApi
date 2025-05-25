namespace AttendanceApi.Models;

public class Student
{
    public int Id { get; set; }
    public required string Roll { get; set; }
    public required string Name { get; set; }
    public int StudentBatchId { get; set; }
    
    public required StudentBatch StudentBatch { get; set; }
    public ICollection<AttendanceRecord> AttendanceRecords { get; set; } = new List<AttendanceRecord>();
}