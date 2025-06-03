using AttendanceApi.Models;

namespace AttendanceApi.Interfaces;

public interface ISessionRepo
{
    Task<Session?> GetSessionByIdAsync(int sessionId);
    Task<bool> CreateSessionAsync(Session session);
    Task<bool> SaveAsync();
}