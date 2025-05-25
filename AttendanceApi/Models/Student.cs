using System.ComponentModel.DataAnnotations;

namespace AttendanceApi.Models;

public class Student
{
    public int Id { get; set; }
    [MaxLength(12)]
    public required string Roll { get; set; }
    [MaxLength(50)]
    public required string Name { get; set; }
    public int StudentBatchId { get; set; }
    
    public StudentBatch? StudentBatch { get; set; }
    public ICollection<AttendanceRecord> AttendanceRecords { get; set; } = new List<AttendanceRecord>();
}