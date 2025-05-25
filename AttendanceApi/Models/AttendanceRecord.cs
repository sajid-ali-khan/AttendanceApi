namespace AttendanceApi.Models;

public class AttendanceRecord
{
    public int SessionId { get; set; }
    public int StudentId { get; set; }
    public int Status { get; set; }
    
    public required Session Session { get; set; }
    public required Student Student { get; set; }
}