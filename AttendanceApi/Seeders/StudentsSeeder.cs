using AttendanceApi.Data;
using AttendanceApi.InitialData;
using AttendanceApi.Models;
using AttendanceApi.Models.Enums;

namespace AttendanceApi.Seeders;

public class StudentsSeeder
{
    public static void Seed(CollegeDbContext context, StructuredCollegeDbContext newContext)
    {
        var rawStudents = context.RawStudents.ToList();
        
        var students = new List<Student>();
        foreach (var rs in rawStudents)
        {
            var degree = (rs.Degr!.StartsWith('B')) ? Degree.Btech : Degree.Mtech;
            var branchId = rs.Branch / 10;
            var scheme = newContext.Schemes.FirstOrDefault(s => s.Name == rs.Scheme);

            var studentBatch = newContext.StudentBatches
                .FirstOrDefault(sb => (
                    sb.OfferedProgram.Degree == degree &&
                    sb.OfferedProgram.BranchId == branchId &&
                    sb.OfferedProgram.SchemeId == scheme!.Id &&
                    sb.Semester == rs.Sem &&
                    sb.Section == rs.Sec
                ));

            if (studentBatch is null)
            {
                ThrowException($"A studentbatch with branch = {branchId}, scheme = {scheme!.Id}, semester = {rs.Sem} and section = {rs.Sec} doesn't exists!!");
                
            }

            var student = new Student
            {
                Roll = rs.Rollno,
                Name = rs.Name,
                StudentBatchId = studentBatch!.Id,
                StudentBatch = studentBatch
            };

            students.Add(student);
        }

        newContext.Students.AddRange(students);
        newContext.SaveChanges();
        
        Console.WriteLine("✅Seeding Students finished!!");
    }
    
    private static void ThrowException(string msg)
    {
        throw new Exception(msg);
    }
}