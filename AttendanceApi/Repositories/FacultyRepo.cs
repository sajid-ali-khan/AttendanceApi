using AttendanceApi.Data;
using AttendanceApi.Interfaces;
using AttendanceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendanceApi.Repositories;

public class FacultyRepo: IFacultyRepo
{
    private readonly StructuredCollegeDbContext _context;

    public FacultyRepo(StructuredCollegeDbContext context)
    {
        _context = context;
    }
    public async Task<ICollection<Faculty>> GetFaculties()
    {
        return await _context.Faculties.ToListAsync();
    }

    public async Task<Faculty?> GetFaculty(int facultyId)
    {
        return await _context.Faculties.FirstOrDefaultAsync(f => f.Id == facultyId);
    }
}