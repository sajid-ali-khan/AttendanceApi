using AttendanceApi.Models.Enums;

namespace AttendanceApi.Models;

public class OfferedProgram
{
    public int Id { get; set; }
    public Degree Degree { get; set; }
    public int SchemeId { get; set; }
    public int BranchId { get; set; }
    
    public required Scheme Scheme { get; set; }
    public required Branch Branch { get; set; }
    public ICollection<StudentBatch>? StudentBatches { get; set; } = new List<StudentBatch>();
}