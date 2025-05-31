using AttendanceApi.Models;

namespace AttendanceApi.Interfaces;

public interface IFacultyRepo
{
    public Task<ICollection<Faculty>> GetFaculties();
    public Task<Faculty?> GetFaculty(int facultyId);
}