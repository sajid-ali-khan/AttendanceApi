using AttendanceApi.Models;

namespace AttendanceApi.Interfaces;

public interface ICourseAssignmentRepo
{
    Task<CourseAssignment?> GetCourseAssignmentById(int courseId);
    Task<ICollection<CourseAssignment>> GetCourseAssignments();
    Task<bool> CreateCourseAssignment(CourseAssignment courseAssignment);
    Task<bool> CourseAssignmentExists(int courseId, int facultyId);
    Task<bool> CourseAssignmentExists(int courseAssignmentId);
    Task<bool> Save();
}