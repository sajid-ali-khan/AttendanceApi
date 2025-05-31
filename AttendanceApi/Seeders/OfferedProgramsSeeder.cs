using AttendanceApi.Data;
using AttendanceApi.InitialData;
using AttendanceApi.Models;
using AttendanceApi.Models.Enums;

namespace AttendanceApi.Seeders;

public class OfferedProgramsSeeder
{
    public static void Seed(CollegeDbContext context, StructuredCollegeDbContext newContext)
    {
        var distinctPrograms = context.RawCourses
            .Select(rc => new
            {
                Degree = rc.Degr,
                Scheme = rc.Scheme,
                BranchId = rc.Branch / 10
            })
            .Distinct()
            .ToList();

        var correctPrograms = new List<OfferedProgram>();

        foreach (var dp in distinctPrograms)
        {
            var degree = dp.Degree.StartsWith('B') ? Degree.Btech : Degree.Mtech;
            var scheme = newContext.Schemes
                .FirstOrDefault(s => s.Name == dp.Scheme);
            var branch = newContext.Branches.FirstOrDefault(b => b.Id == dp.BranchId);

            if (scheme is null || branch is null)
            {
                ThrowException("Either scheme or branch is null/NotFound from RawCourses");
            }
            
            var exists = newContext.OfferedPrograms.Any(op =>
                op.BranchId == dp.BranchId &&
                op.SchemeId == scheme!.Id &&
                op.Degree == degree);

            if (exists)
            {
                ThrowException($"⚠️ Skipped existing OfferedProgram: {dp.Degree}, {dp.Scheme}, BranchId {dp.BranchId}");
            }

            var offeredProgram = new OfferedProgram
            {
                Degree = degree,
                Scheme = scheme!,
                Branch = branch!,
                SchemeId = scheme!.Id,
                BranchId = branch!.Id,
            };
            correctPrograms.Add(offeredProgram);
        }
        
        newContext.OfferedPrograms.AddRange(correctPrograms);
        newContext.SaveChanges();
        
        Console.WriteLine("✅ Fininshed seeding offered programs.");
    }
    
    private static void ThrowException(string msg)
    {
        throw new Exception(msg);
    }
}