using AttendanceApi.Models;
using AttendanceApi.Models.Enums;

namespace AttendanceApi.Interfaces;

public interface ISchemeRepo
{
    ICollection<Scheme> GetSchemes();
    ICollection<OfferedProgram>? GetOfferedPrograms(int schemeId);
}