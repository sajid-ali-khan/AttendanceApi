using AttendanceApi.Data;
using AttendanceApi.Interfaces;
using AttendanceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendanceApi.Repositories;

public class OfferedProgramRepo: IOfferedProgramRepo
{
    private readonly StructuredCollegeDbContext _context;

    public OfferedProgramRepo(StructuredCollegeDbContext context)
    {
        _context = context;
    }
    public async Task<ICollection<OfferedProgram>> GetOfferedProgramsForScheme(int schemeId)
    {
        return await _context.OfferedPrograms
            .Include(op => op.Branch)
            .Where(op => op.SchemeId == schemeId)
            .ToListAsync();
    }
}