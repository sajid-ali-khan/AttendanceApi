using AttendanceApi.Models;

namespace AttendanceApi.Interfaces;

public interface IFacultyRepo
{
    Task<ICollection<Faculty>> GetFaculties();
    Task<Faculty> GetFacultyById(int facultyId);
    Task<bool> FacultyExists(int facultyId);
}