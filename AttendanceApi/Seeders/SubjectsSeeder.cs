using AttendanceApi.Data;
using AttendanceApi.InitialData;
using AttendanceApi.Models;
using AttendanceApi.Models.Enums;

namespace AttendanceApi.Seeders;

public class SubjectsSeeder
{
    public static void Seed(CollegeDbContext context, StructuredCollegeDbContext newContext)
    {
        var rawSubjects = context.RawCourses
            .Select(rc => new
            {
                rc.Scode,
                rc.Subname
            })
            .Distinct()
            .ToList();

        var subjects = rawSubjects
            .Select(rs => new Subject
            {
                ShortName = rs.Scode.Trim(),
                FullName = rs.Subname.Trim(),
                SubjectType = (rs.Scode.EndsWith("(P)")) ? SubjectType.Lab : SubjectType.Theory
            })
            .ToList();
        
        newContext.Subjects.AddRange(subjects);
        newContext.SaveChanges();
        Console.WriteLine($"✅ Seeded {subjects.Count} subjects.");
    }
}