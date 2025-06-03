using AttendanceApi.Dtos;
using AttendanceApi.Interfaces;
using AttendanceApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CourseAssignmentController: Controller
{
    private readonly ICourseAssignmentRepo _caRepo;
    private readonly IMapper _mapper;
    private readonly ICourseRepo _courseRepo;
    private readonly IFacultyRepo _facultyRepo;

    public CourseAssignmentController(
        ICourseAssignmentRepo caRepo,
        IMapper mapper,
        ICourseRepo courseRepo,
        IFacultyRepo facultyRepo
        )
    {
        _caRepo = caRepo;
        _mapper = mapper;
        _courseRepo = courseRepo;
        _facultyRepo = facultyRepo;
    }

    [HttpGet("{courseAssignmentId}")]
    [ProducesResponseType(200, Type = typeof(CourseAssignmentOutputDtoSingle))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetCourseAssignmentById(int courseAssignmentId)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var exists = await _caRepo.CourseAssignmentExists(courseAssignmentId);
        if (!exists)
            return NotFound();
        var courseAssignmentEntity = await _caRepo.GetCourseAssignmentById(courseAssignmentId);
        var courseAssignmentDto = _mapper.Map<CourseAssignmentOutputDtoSingle>(courseAssignmentEntity);
        return Ok(courseAssignmentDto);
    }
    
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<CourseAssignmentOutputDtoSingle>))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetCourseAssignments()
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var courseAssignmentEntities = await _caRepo.GetCourseAssignments();
        var courseAssignmentDtos = _mapper.Map<ICollection<CourseAssignmentOutputDtoSingle>>(courseAssignmentEntities);
        return Ok(courseAssignmentDtos);
    }


    [HttpPost]
    [ProducesResponseType(201, Type = typeof(Object))]
    [ProducesResponseType(200, Type = typeof(Object))]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> CreateCourseAssignment([FromBody] CourseAssignmentCreateDto newCourseAssignment)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        var courseExists = await _courseRepo.CourseExists(newCourseAssignment.CourseId);
        if (!courseExists)
            return BadRequest(new
                { message = $"A course with courseId = {newCourseAssignment.CourseId} doesn't exist." });

        var facultyExists = await _facultyRepo.FacultyExists(newCourseAssignment.FacultyId);
        if (!facultyExists)
            return BadRequest(new
            {
                message = $"A faculty with facultyId = {newCourseAssignment.FacultyId} doesn't exist."
            });
        
        var alreadyExists = await _caRepo.CourseAssignmentExists(newCourseAssignment.CourseId, newCourseAssignment.FacultyId);
        
        if (alreadyExists)
            return Conflict(new { message= "The resource already exists" });
        
        var course = await _courseRepo.GetCourseById(newCourseAssignment.CourseId);
        var faculty = await _facultyRepo.GetFacultyById(newCourseAssignment.FacultyId);

        var courseAssignment = _mapper.Map<CourseAssignment>(newCourseAssignment);
        courseAssignment.Course = course;
        courseAssignment.Faculty = faculty;
        
        var saved = await _caRepo.CreateCourseAssignment(courseAssignment);
        if (!saved)
            return StatusCode(500, new { message = "Oops! Something went wrong" });

        var courseAssignmentDto = _mapper.Map<CourseAssignmentOutputDtoSingle>(courseAssignment);
        return CreatedAtAction(
            nameof(GetCourseAssignmentById),
            new { courseAssignmentId = courseAssignment.Id },
            courseAssignmentDto
        );
    }
}