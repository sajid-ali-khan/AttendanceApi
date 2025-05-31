using AttendanceApi.Models;

namespace AttendanceApi.Interfaces;

public interface IOfferedProgramRepo
{
    Task<ICollection<OfferedProgram>> GetOfferedProgramsForSchemeId(int schemeId);
    Task<ICollection<StudentBatch>?> GetStudentBatchesForProgramId(int programId);
}