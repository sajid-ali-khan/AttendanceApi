namespace AttendanceApi.Models;

public class Session
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public int FacultyId { get; set; }
    public int NumPresent { get; set; }
    public int NumAbsent { get; set; }
    public DateOnly UpdatedDate { get; set; }
    public string TimeStamp { get; set; } = DateTime.Now.ToString("yyyyMMddHHmmss");
    
    public ICollection<AttendanceRecord> AttendanceRecords { get; set; } = new List<AttendanceRecord>();
}