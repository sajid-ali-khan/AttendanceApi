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

    public async Task<Faculty> GetFacultyById(int facultyId)
    {
        return await _context.Faculties.FirstAsync(f => f.Id == facultyId);
    }

    public async Task<bool> FacultyExists(int facultyId)
    {
        return await _context.Faculties.AnyAsync(f => f.Id == facultyId);
    }
}