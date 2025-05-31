using AttendanceApi.Models;

namespace AttendanceApi.Interfaces;

public interface IStudentBatchRepo
{
    public Task<ICollection<Course>?> GetCoursesForStudentBatchId(int studentBatchId);
}