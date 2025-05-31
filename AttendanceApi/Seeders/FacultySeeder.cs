using AttendanceApi.Data;
using AttendanceApi.InitialData;
using AttendanceApi.Models;
using AttendanceApi.Models.Enums;

namespace AttendanceApi.Seeders;

public class FacultySeeder
{
    public static void Seed(CollegeDbContext context, StructuredCollegeDbContext newContext)
    {
        var rawFaculties = context.RawEmployees.ToList();

        var faculties = rawFaculties
            .Select(rf => new Faculty
            {
                Code = rf.Empid.ToString(),
                Name = rf.Name,
                PasswordHash = rf.Pwd.ToString(),
                Role = FacultyRole.Teacher
            })
            .ToList();
        
        newContext.Faculties.AddRange(faculties);
        newContext.SaveChanges();
        
        Console.WriteLine("✅Seeding faculties finished!!");
    }
}