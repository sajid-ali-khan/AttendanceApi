using AttendanceApi.Data;
using AttendanceApi.Interfaces;
using AttendanceApi.Models;

namespace AttendanceApi.Repositories;

public class SchemeRepo: ISchemeRepo
{
    private readonly StructuredCollegeDbContext _context;

    public SchemeRepo(StructuredCollegeDbContext context)
    {
        _context = context;
    }
    public ICollection<Scheme> GetSchemes()
    {
        return _context.Schemes.ToList();
    }

    public ICollection<OfferedProgram>? GetOfferedPrograms(int schemeId)
    {
        var offeredPrograms = _context.Schemes
            .Where(sch => sch.Id == schemeId)
            .Select(sch => sch.OfferedPrograms)
            .FirstOrDefault();
        return offeredPrograms;
    }
}