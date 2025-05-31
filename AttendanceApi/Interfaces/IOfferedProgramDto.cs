using AttendanceApi.Models;

namespace AttendanceApi.Interfaces;

public interface IOfferedProgramDto
{
    public Task<ICollection<OfferedProgram>> GetOfferedProgramsForScheme(int schemeId);
}