using AttendanceApi.Dtos;
using AttendanceApi.Interfaces;
using AttendanceApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentBatchController : Controller
{
    private readonly IStudentBatchRepo _studentBatchRepo;
    private readonly IMapper _mapper;

    public StudentBatchController(IStudentBatchRepo studentBatchRepo, IMapper mapper)
    {
        _studentBatchRepo = studentBatchRepo;
        _mapper = mapper;
    }

    [HttpGet("{studentBatchId}/Courses")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Course>))]
    public async Task<IActionResult> GetCoursesForStudentBatchId(int studentBatchId)
    {
        var courseEntities = await _studentBatchRepo.GetCoursesForStudentBatchId(studentBatchId);
        var courseDtos = _mapper.Map<IEnumerable<CourseOutputDto>>(courseEntities);
        return Ok(courseDtos);
    }

    [HttpGet("{studentBatchId}/courseAssignments")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<CourseAssignmentForAClassDto>))]
    public async Task<IActionResult> GetCourseAssignmentsForStudentBatchId(int studentBatchId)
    {
        var caEntities = await _studentBatchRepo.GetCourseAssignmentsForStudentBatchId(studentBatchId);
        var courseAssignments = _mapper.Map<IEnumerable<CourseAssignmentForAClassDto>>(caEntities);
        return Ok(courseAssignments);
    }
}