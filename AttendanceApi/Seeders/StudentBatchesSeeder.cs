using AttendanceApi.Data;
using AttendanceApi.InitialData;
using AttendanceApi.Models;
using AttendanceApi.Models.Enums;

namespace AttendanceApi.Seeders;

public class StudentBatchesSeeder
{
    public static void Seed(CollegeDbContext context, StructuredCollegeDbContext newContext)
    {
        var batches = context.RawStudents
            .Select(rs => new
            {
                Degree = rs.Degr,
                Scheme = rs.Scheme,
                BranchId = rs.Branch / 10,
                Year = rs.Branch % 10,
                Semester = rs.Sem,
                Section = rs.Sec
            })
            .Distinct()
            .OrderBy(rs => rs.BranchId)
            .ThenBy(rs => rs.Scheme)
            .ThenBy(rs => rs.Semester)
            .ThenBy(rs => rs.Section)
            .ToList();

        var studentBatches = new List<StudentBatch>();

        foreach (var batch in batches)
        {
            var degree = batch.Degree!.StartsWith('B') ? Degree.Btech : Degree.Mtech;

            var scheme = newContext.Schemes.FirstOrDefault(s => s.Name == batch.Scheme);
            if (scheme == null)
            {
                ThrowException($"❌ Scheme not found: {batch.Scheme}");
            }

            var offeredProgram = newContext.OfferedPrograms.FirstOrDefault(op =>
                op.Degree == degree &&
                op.SchemeId == scheme!.Id &&
                op.BranchId == batch.BranchId);

            if (offeredProgram == null)
            {
                ThrowException($"❌ OfferedProgram not found for: {batch.Degree}, BranchId {batch.BranchId}, Scheme {batch.Scheme}");
                
            }

            bool alreadyExists = newContext.StudentBatches.Any(sb =>
                sb.OfferedProgramId == offeredProgram!.Id &&
                sb.Semester == batch.Semester &&
                sb.Section == batch.Section);

            if (alreadyExists)
            {
                ThrowException($"⚠️ Skipping duplicate StudentBatch: ProgramId={offeredProgram!.Id}, Sem={batch.Semester}, Sec={batch.Section}");
                
            }

            studentBatches.Add(new StudentBatch
            {
                OfferedProgramId = offeredProgram!.Id,
                Semester = batch.Semester,
                Section = batch.Section,
                OfferedProgram = offeredProgram,
            });
        }

        newContext.StudentBatches.AddRange(studentBatches);
        newContext.SaveChanges();

        Console.WriteLine($"✅ Seeded {studentBatches.Count} StudentBatches.");
    }
    
    private static void ThrowException(string msg)
    {
        throw new Exception(msg);
    }
}