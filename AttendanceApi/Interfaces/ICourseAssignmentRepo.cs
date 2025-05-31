using AttendanceApi.Models;

namespace AttendanceApi.Interfaces;

public interface ICourseAssignmentRepo
{
    Task<CourseAssignment?> GetCourseAssignment(int courseId);
    Task<bool> CreateCourseAssignment(CourseAssignment courseAssignment);
    Task<bool> CourseAssignmentExists(int courseId, int facultyId);
    Task<bool> CourseAssignmentExists(int courseAssignmentId);
    Task<bool> Save();
}