using AttendanceApi.Models;

namespace AttendanceApi.Interfaces;

public interface IStudentBatchRepo
{
    Task<ICollection<Course>?> GetCoursesForStudentBatchId(int studentBatchId);
}