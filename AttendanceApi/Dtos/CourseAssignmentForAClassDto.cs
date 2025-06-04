namespace AttendanceApi.Dtos;

public class CourseAssignmentForAClassDto
{
    public int Id { get; set; }
    public string SubjectShortName { get; set; }
    public ICollection<FacultyInCourseAssignmentDto> CourseAssignments { get; set; } = new List<FacultyInCourseAssignmentDto>();
}