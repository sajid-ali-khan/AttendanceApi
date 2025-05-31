using AttendanceApi.Models;

namespace AttendanceApi.Interfaces;

public interface IOfferedProgramRepo
{
    public Task<ICollection<OfferedProgram>> GetOfferedProgramsForSchemeId(int schemeId);
    public Task<ICollection<StudentBatch>?> GetStudentBatchesForProgramId(int programId);
}