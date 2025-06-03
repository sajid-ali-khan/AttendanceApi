namespace AttendanceApi.Dtos;

public class SessionCreateDto
{
    public int CourseId { get; set; }
    public int FacultyId { get; set; }
    public DateOnly UpdatedDate { get; set; } = DateOnly.FromDateTime(DateTime.Now.Date);
    public string TimeStamp { get; set; } = DateTime.Now.ToString("yyyyMMddHHmmss");
}