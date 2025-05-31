using AttendanceApi.Data;
using AttendanceApi.Interfaces;
using AttendanceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendanceApi.Repositories;

public class SchemeRepo: ISchemeRepo
{
    private readonly StructuredCollegeDbContext _context;

    public SchemeRepo(StructuredCollegeDbContext context)
    {
        _context = context;
    }
    public async Task<ICollection<Scheme>> GetSchemes()
    {
        return await _context.Schemes.ToListAsync();
    }

    public async Task<ICollection<OfferedProgram>?> GetOfferedPrograms(int schemeId)
    {
        var scheme = await _context.Schemes
            .Include(sch => sch.OfferedPrograms)
            .ThenInclude(op => op.Branch)
            .FirstOrDefaultAsync(sch => sch.Id == schemeId);
        return scheme?.OfferedPrograms;
    }
}