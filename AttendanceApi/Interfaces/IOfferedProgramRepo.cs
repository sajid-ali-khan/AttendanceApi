using AttendanceApi.Models;

namespace AttendanceApi.Interfaces;

public interface IOfferedProgramRepo
{
    public Task<ICollection<OfferedProgram>> GetOfferedProgramsForScheme(int schemeId);
}