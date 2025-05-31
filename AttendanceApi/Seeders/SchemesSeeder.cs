using AttendanceApi.Data;
using AttendanceApi.InitialData;
using AttendanceApi.Models;

namespace AttendanceApi.Seeders;

public class SchemesSeeder
{
    public static void Seed(CollegeDbContext context, StructuredCollegeDbContext newContext)
    {
        var rawSchemes = context.RawCourses
            .Select(rc => rc.Scheme)
            .Distinct()
            .ToList();

        var newSchemes = rawSchemes
            .Select(rs => new Scheme { Name = rs })
            .ToList();

        newContext.Schemes.AddRange(newSchemes);
        newContext.SaveChanges();
        
        Console.WriteLine("✅Schemes Seeded!!");
    }
}