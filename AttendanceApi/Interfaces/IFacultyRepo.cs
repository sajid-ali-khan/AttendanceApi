using AttendanceApi.Models;

namespace AttendanceApi.Interfaces;

public interface IFacultyRepo
{
    Task<ICollection<Faculty>> GetFaculties();
    Task<Faculty?> GetFaculty(int facultyId);
}