using AttendanceApi.Data;
using AttendanceApi.Interfaces;
using AttendanceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendanceApi.Repositories;

public class StudentBatchRepo: IStudentBatchRepo
{
    private readonly StructuredCollegeDbContext _context;

    public StudentBatchRepo(StructuredCollegeDbContext context)
    {
        _context = context;
    }
    public async Task<ICollection<Course>?> GetCoursesForStudentBatchId(int studentBatchId)
    {
        var studentBatch = await _context.StudentBatches
            .Include(sb => sb.Courses)
            .ThenInclude(c => c.Subject)
            .FirstOrDefaultAsync(sb => sb.Id == studentBatchId);
        return studentBatch?.Courses;
    }
}