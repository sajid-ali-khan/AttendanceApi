using AttendanceApi.Data;
using AttendanceApi.Interfaces;
using AttendanceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendanceApi.Repositories;


public class CourseRepo(StructuredCollegeDbContext context) : ICourseRepo
{
    public async Task<bool> CourseExists(int courseId)
    {
        return await context.Courses.AnyAsync(c => c.Id == courseId);
    }

    public async  Task<Course> GetCourseById(int courseId)
    {
        return await context.Courses
            .Include(c => c.StudentBatch)
            .ThenInclude(sb => sb.OfferedProgram)
            .ThenInclude(op => op.Branch)
            .Include(c => c.Subject)
            .FirstAsync(c => c.Id == courseId);
    }

    public async Task<int> StudentCount(int courseId)
    {
        return await context.Courses.Where(c => c.Id == courseId)
            .Select(c => c.StudentBatch)
            .Select(s => s.Students)
            .CountAsync();
    }
}