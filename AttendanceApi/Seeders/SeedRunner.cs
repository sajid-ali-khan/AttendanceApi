using AttendanceApi.Data;
using AttendanceApi.InitialData;

namespace AttendanceApi.Seeders;

public static class SeederRunner
{
    public static void Run(CollegeDbContext context, StructuredCollegeDbContext newContext)
    {
        SchemesSeeder.Seed(context, newContext);
        OfferedProgramsSeeder.Seed(context, newContext);
        StudentBatchesSeeder.Seed(context, newContext);
        SubjectsSeeder.Seed(context, newContext);
        CoursesSeeder.Seed(context, newContext);
        StudentsSeeder.Seed(context, newContext);
        FacultySeeder.Seed(context, newContext);
    }
}