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

    public async Task<ICollection<CourseAssignment>> GetCourseAssignmentsForFacultyAsync(int facultyId)
    {
        return await _context.Faculties
            .Where(f => f.Id == facultyId)
            .SelectMany(f => f.CourseAssignments)
            .Include(f => f.Course)
            .Include(ca => ca.Course)
            .ThenInclude(c => c.StudentBatch)
            .ThenInclude(sb => sb.OfferedProgram)
            .ThenInclude(op => op.Branch)
            .Include(ca => ca.Faculty)
            .Include(ca => ca.Course.Subject)
            .ToListAsync();
    }
}