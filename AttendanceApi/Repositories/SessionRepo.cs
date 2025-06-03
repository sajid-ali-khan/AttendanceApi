using AttendanceApi.Data;
using AttendanceApi.Interfaces;
using AttendanceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendanceApi.Repositories;

public class SessionRepo(StructuredCollegeDbContext context): ISessionRepo
{
    public async Task<Session?> GetSessionByIdAsync(int sessionId)
    {
        return await context.Sessions.FirstOrDefaultAsync(s => s.Id == sessionId);
    }

    public async Task<bool> CreateSessionAsync(Session session)
    {
        await context.Sessions.AddAsync(session);
        return await SaveAsync();
    }

    public async Task<bool> SaveAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }
}