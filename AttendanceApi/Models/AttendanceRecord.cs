namespace AttendanceApi.Models;

public class AttendanceRecord
{
    public int SessionId { get; set; }
    public int StudentId { get; set; }
    public AttendanceStatus Status { get; set; } = AttendanceStatus.Absent;
    
    public Session? Session { get; set; }
    public Student? Student { get; set; }
}

public enum AttendanceStatus
{
    Absent = 0,
    Present = 1
}