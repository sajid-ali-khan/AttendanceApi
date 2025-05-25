using AttendanceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendanceApi.Data;

public class StructuredCollegeDbContext: DbContext
{
    public StructuredCollegeDbContext(DbContextOptions<StructuredCollegeDbContext> options): base(options)
    {
    }

    public DbSet<AttendanceRecord> AttendanceRecords { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<CourseAssignment> CourseAssignments { get; set; }
    public DbSet<Faculty> Faculties { get; set; }
    public DbSet<OfferedProgram> OfferedPrograms { get; set; }
    public DbSet<Scheme> Schemes { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<StudentBatch> StudentBatches { get; set; }
    public DbSet<Subject> Subjects { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AttendanceRecord>(entity =>
        {
            entity.HasKey(ar => new { ar.SessionId, ar.StudentId });

            entity.Property(ar => ar.Status)
                .HasDefaultValue(AttendanceStatus.Absent);
        });

        modelBuilder.Entity<Branch>(entity =>
        {
            entity.HasKey(b => b.Id);
        });
        
        InitializeBranchesTable(modelBuilder);

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.HasMany(c => c.CourseAssignments)
                .WithOne(ca => ca.Course)
                .HasForeignKey(ca => ca.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasMany(c => c.Sessions)
                .WithOne(session => session.Course)
                .HasForeignKey(session => session.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<CourseAssignment>(entity =>
        {
            entity.HasKey(ca => ca.Id);
        });

        modelBuilder.Entity<Faculty>(entity =>
        {
            entity.HasKey(f => f.Id);
            entity.HasMany(f => f.CourseAssignments)
                .WithOne(ca => ca.Faculty)
                .HasForeignKey(ca => ca.FacultyId)
                .OnDelete(DeleteBehavior.Restrict); //don't delete the CA's when faculty is deleted, mark with null instead
            
            entity.HasMany(f => f.Sessions)
                .WithOne(session => session.Faculty)
                .HasForeignKey(session => session.FacultyId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<OfferedProgram>(entity =>
        {
            entity.HasKey(op => op.Id);

            entity.HasMany(op => op.StudentBatches)
                .WithOne(sb => sb.OfferedProgram)
                .HasForeignKey(sb => sb.OfferedProgramId)
                .OnDelete(DeleteBehavior.Cascade); //delete all section upon deletion of branch
        });

        modelBuilder.Entity<Scheme>(entity =>
        {
            entity.HasKey(scheme => scheme.Id);
            entity.HasMany(scheme => scheme.OfferedPrograms)
                .WithOne(op => op.Scheme)
                .HasForeignKey(op => op.SchemeId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(session => session.Id);
            entity.HasMany(session => session.AttendanceRecords)
                .WithOne(ar => ar.Session)
                .HasForeignKey(ar => ar.SessionId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(student => student.Id);
            entity.HasMany(student => student.AttendanceRecords)
                .WithOne(ar => ar.Student)
                .HasForeignKey(ar => ar.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<StudentBatch>(entity =>
        {
            entity.HasKey(sb => sb.Id);
            entity.HasMany(sb => sb.Courses)
                .WithOne(c => c.StudentBatch)
                .HasForeignKey(c => c.StudentBatchId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(sb => sb.Students)
                .WithOne(s => s.StudentBatch)
                .HasForeignKey(s => s.StudentBatchId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(subject => subject.Id);
            entity.HasMany(sub => sub.Courses)
                .WithOne(c => c.Subject)
                .HasForeignKey(c => c.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

    private static void InitializeBranchesTable(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Branch>().HasData(
            new Branch() {Id = 1, ShortName = "CSE", FullName = "Computer Science & Engineering"},
            new Branch() {Id = 2, ShortName = "CIV", FullName = "Civil Engineering"},
            new Branch() {Id = 3, ShortName = "CST", FullName = "Computer Science & Technology"},
            new Branch() {Id = 4, ShortName = "ECE", FullName = "Electronics and Communication Engineering"},
            new Branch() {Id = 5, ShortName = "MEC", FullName = "Mechanical Engineering"},
            new Branch() {Id = 6, ShortName = "CSB", FullName = "Computer Science & Business Systems"},
            new Branch() {Id = 7, ShortName = "EEE", FullName = "Electrical and Electronics Engineering"},
            new Branch() {Id = 8, ShortName = "CSD", FullName = "Data Science"},
            new Branch() {Id = 9, ShortName = "CSM", FullName = "Artificial Intelligence & Machine Learning"}
        );
    }
}