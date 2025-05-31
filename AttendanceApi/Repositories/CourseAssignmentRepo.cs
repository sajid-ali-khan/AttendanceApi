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

    public async Task<CourseAssignment?> GetCourseAssignment(int courseId)
    {
        return await _context.CourseAssignments.FirstOrDefaultAsync(c => c.CourseId == courseId);
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