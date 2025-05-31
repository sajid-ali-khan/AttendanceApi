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
    public async Task<ICollection<OfferedProgram>> GetOfferedProgramsForSchemeId(int schemeId)
    {
        return await _context.OfferedPrograms
            .Include(op => op.Branch)
            .Where(op => op.SchemeId == schemeId)
            .ToListAsync();
    }

    public async Task<ICollection<StudentBatch>?> GetStudentBatchesForProgramId(int programId)
    {
        var offeredProgram = await _context.OfferedPrograms
            .Include(op => op.StudentBatches)
            .FirstOrDefaultAsync(op => op.Id == programId);
        return offeredProgram?.StudentBatches;
    }
}