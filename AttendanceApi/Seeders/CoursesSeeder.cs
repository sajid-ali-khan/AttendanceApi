using AttendanceApi.Data;
using AttendanceApi.InitialData;
using AttendanceApi.Models;
using AttendanceApi.Models.Enums;

namespace AttendanceApi.Seeders;

public class CoursesSeeder
{
    public static void Seed(CollegeDbContext context, StructuredCollegeDbContext newContext)
    {
        var rawCourses = context.RawCourses.ToList();
        var courses = new List<Course>();
        foreach (var rc in rawCourses)
        {
            var degree = (rc.Degr.StartsWith('B')) ? Degree.Btech : Degree.Mtech;
            var scheme = newContext.Schemes.FirstOrDefault(s => s.Name == rc.Scheme);
            var branchId = rc.Branch / 10;
            var semester = rc.Sem;
            var scode = rc.Scode;
            var subname = rc.Subname;

            var offeredProgram = newContext.OfferedPrograms
                .FirstOrDefault(op => (op.Degree == degree && op.SchemeId == scheme!.Id && op.BranchId == branchId));
            if (offeredProgram is null)
            {
                ThrowException(
                    $"A program with degree = {degree}, scheme = {scheme!.Name}, branch = {branchId} doesn't exist!!");
            }

            var studentBatch = newContext.StudentBatches
                .FirstOrDefault(sb => (sb.OfferedProgramId == offeredProgram!.Id && sb.Semester == semester));

            var subject = newContext.Subjects
                .FirstOrDefault(sub => (sub.ShortName == scode && sub.FullName == subname));

            if (subject is null)
            {
                ThrowException($"A subject with scheme = {scode}, branch = {branchId} doesn't exist!!");
            }

            if (studentBatch is null)
            {
                
                Console.WriteLine($"A program with degree = {degree}, scheme = {scheme!.Name}, branch = {branchId} does exist!! But"+"\n"+$"A student batch with offeredProgram = {offeredProgram!.Id}, semester = {semester} doesn't exist!!");
                continue;
            }

            var course = new Course
            {
                StudentBatchId = studentBatch!.Id,
                StudentBatch = studentBatch,
                SubjectId = subject!.Id,
                Subject = subject,
            };
            courses.Add(course);
        }
        
        newContext.Courses.AddRange(courses);
        newContext.SaveChanges();
        
        Console.WriteLine("✅Course Seeder has been finished.");
    }

    private static void ThrowException(string msg)
    {
        throw new Exception(msg);
    }
}