using AttendanceApi.Data;
using AttendanceApi.Interfaces;
using AttendanceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendanceApi.Repositories;

public class SessionRepo(StructuredCollegeDbContext context): ISessionRepo
{
    public async Task<Session?> GetSessionByIdAsync(int sessionId)
    {
        return await context.Sessions
            .Include(s => s.Course)
            .ThenInclude(c => c.StudentBatch)
            .ThenInclude(sb => sb.OfferedProgram)
            .ThenInclude(op => op.Branch)
            .Include(s => s.Faculty)
            .FirstOrDefaultAsync(s => s.Id == sessionId);
    }

    public async Task<bool> CreateSessionAsync(Session session)
    {
        await using var transaction = await context.Database.BeginTransactionAsync();

        try
        {
            // Add session but don't save yet
            await context.Sessions.AddAsync(session);
            await context.SaveChangesAsync(); // This gets the generated session.Id
        
            Console.WriteLine($"The id of newly created session = {session.Id}");

            // Get student IDs
            var studentIdList = await context.Students
                .Where(s => s.StudentBatchId == session.Course!.StudentBatchId)
                .Select(s => s.Id)
                .ToListAsync();

            if (studentIdList.Count == 0)
            {
                await transaction.RollbackAsync();
                return false;
            }

            // Create attendance records
            var attendanceRecords = studentIdList.Select(studentId => new AttendanceRecord
            {
                SessionId = session.Id,
                StudentId = studentId
            });

            await context.AttendanceRecords.AddRangeAsync(attendanceRecords);
            var recordsSaved = await context.SaveChangesAsync();

            // Commit only if all records were saved
            if (recordsSaved == studentIdList.Count)
            {
                await transaction.CommitAsync();
                return true;
            }

            await transaction.RollbackAsync();
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception during session creation: {ex.Message}");
            await transaction.RollbackAsync();
            return false;
        }
    }
    public async Task<bool> SaveAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }
}