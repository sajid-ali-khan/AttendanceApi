using AttendanceApi.Models;

namespace AttendanceApi.Interfaces;

public interface ICourseRepo
{
    Task<bool> CourseExists(int courseId);
    Task<Course> GetCourseById(int courseId);
}