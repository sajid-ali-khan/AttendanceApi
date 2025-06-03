using System.ComponentModel.DataAnnotations;

namespace AttendanceApi.Models;

public class Session
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public int FacultyId { get; set; }
    public int NumPresent { get; set; }
    public int NumAbsent { get; set; }
    public DateOnly UpdatedDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);
    
    public string TimeStamp { get; set; } = DateTime.Now.ToString("yyyyMMddHHmmss");
    
    public required Course Course { get; set; }
    public required Faculty Faculty { get; set; }
    
    public ICollection<AttendanceRecord> AttendanceRecords { get; set; } = new List<AttendanceRecord>();
}