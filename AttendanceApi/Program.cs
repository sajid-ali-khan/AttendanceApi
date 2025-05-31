

using AttendanceApi.Data;
using AttendanceApi.InitialData;
using AttendanceApi.Interfaces;
using AttendanceApi.Repositories;
using AttendanceApi.Seeders;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ISchemeRepo, SchemeRepo>();
builder.Services.AddScoped<IOfferedProgramRepo, OfferedProgramRepo>();
builder.Services.AddScoped<IStudentBatchRepo, StudentBatchRepo>();

builder.Services.AddDbContext<CollegeDbContext>();
builder.Services.AddDbContext<StructuredCollegeDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("mssql"));
});



var app = builder.Build();

if (args.Contains("seed", StringComparer.OrdinalIgnoreCase))
{
    Console.WriteLine("🔁 Running data seeding...");

    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<CollegeDbContext>();
    var newContext = scope.ServiceProvider.GetRequiredService<StructuredCollegeDbContext>();

    await using var transaction = await newContext.Database.BeginTransactionAsync();

    try
    {
        SeederRunner.Run(context, newContext);
        transaction.Commit();

        Console.WriteLine("✅ Seeding completed.");
    }
    catch (Exception e)
    {
        transaction.Rollback();
        Console.WriteLine($"❌ Seeding failed: {e.Message}");
    }
    return; // exit the app after seeding
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

