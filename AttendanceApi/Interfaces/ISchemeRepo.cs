using AttendanceApi.Models;
using AttendanceApi.Models.Enums;

namespace AttendanceApi.Interfaces;

public interface ISchemeRepo
{
    Task<ICollection<Scheme>> GetSchemes();
    Task<ICollection<OfferedProgram>?> GetOfferedPrograms(int schemeId);
}