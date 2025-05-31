using AttendanceApi.Data;
using AttendanceApi.Interfaces;
using AttendanceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendanceApi.Repositories;

public class CourseAssignmentRepo: ICourseAssignmentRepo
{
    private readonly StructuredCollegeDbContext _context;

    public CourseAssignmentRepo(StructuredCollegeDbContext context)
    {
        _context = context;
    }

    public async Task<CourseAssignment?> GetCourseAssignmentById(int courseId)
    {
        return await _context.CourseAssignments
            .Include(ca => ca.Course)
            .ThenInclude(c => c.StudentBatch)
            .ThenInclude(sb => sb.OfferedProgram)
            .ThenInclude(op => op.Branch)
            .Include(ca => ca.Faculty)
            .Include(ca => ca.Course.Subject)
            .FirstOrDefaultAsync(c => c.CourseId == courseId);
    }

    public async Task<ICollection<CourseAssignment>> GetCourseAssignments()
    {
        return await _context.CourseAssignments
            .Include(ca => ca.Course)
            .ThenInclude(c => c.StudentBatch)
            .ThenInclude(sb => sb.OfferedProgram)
            .ThenInclude(op => op.Branch)
            .Include(ca => ca.Faculty)
            .Include(ca => ca.Course.Subject)
            .ToListAsync();
    }

    public async Task<bool> CreateCourseAssignment(CourseAssignment courseAssignment)
    {
        await _context.CourseAssignments.AddAsync(courseAssignment);
        return await Save();
    }

    public async Task<bool> CourseAssignmentExists(int courseId, int facultyId)
    {
        return await _context.CourseAssignments.AnyAsync(ca => ca.CourseId == courseId && ca.FacultyId == facultyId);
    }

    public async Task<bool> CourseAssignmentExists(int courseAssignmentId)
    {
        return await _context.CourseAssignments.AnyAsync(ca => ca.Id == courseAssignmentId);
    }

    public async Task<bool> Save()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}