using System;
using System.Collections.Generic;
using AttendanceApi.OldModels;
using Microsoft.EntityFrameworkCore;

namespace AttendanceApi.InitialData;

public partial class CollegeDbContext : DbContext
{
    public CollegeDbContext()
    {
    }

    public CollegeDbContext(DbContextOptions<CollegeDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<RawCourse> RawCourses { get; set; }

    public virtual DbSet<RawEmployee> RawEmployees { get; set; }

    public virtual DbSet<RawStudent> RawStudents { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:mssql");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RawCourse>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Branch).HasColumnName("BRANCH");
            entity.Property(e => e.Degr)
                .HasMaxLength(50)
                .HasColumnName("DEGR");
            entity.Property(e => e.Scheme)
                .HasMaxLength(50)
                .HasColumnName("SCHEME");
            entity.Property(e => e.Scode)
                .HasMaxLength(50)
                .HasColumnName("SCODE");
            entity.Property(e => e.Sem).HasColumnName("SEM");
            entity.Property(e => e.Subname)
                .HasMaxLength(100)
                .HasColumnName("SUBNAME");
        });

        modelBuilder.Entity<RawEmployee>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Empid).HasColumnName("EMPID");
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .HasColumnName("GENDER");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("NAME");
            entity.Property(e => e.Pwd).HasColumnName("PWD");
            entity.Property(e => e.Salu)
                .HasMaxLength(50)
                .HasColumnName("SALU");
        });

        modelBuilder.Entity<RawStudent>(entity =>
        {
            entity.HasKey(e => e.Rollno);

            entity.Property(e => e.Rollno)
                .HasMaxLength(50)
                .HasColumnName("ROLLNO");
            entity.Property(e => e.Branch).HasColumnName("BRANCH");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("NAME");
            entity.Property(e => e.Sec)
                .HasMaxLength(50)
                .HasColumnName("SEC");
            entity.Property(e => e.Sem).HasColumnName("SEM");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
