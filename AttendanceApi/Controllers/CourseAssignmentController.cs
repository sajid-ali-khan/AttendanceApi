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

    public CourseAssignmentController(ICourseAssignmentRepo caRepo, IMapper mapper)
    {
        _caRepo = caRepo;
        _mapper = mapper;
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
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(403)]
    public async Task<IActionResult> CreateCourseAssignment([FromBody] CourseAssignmentCreateDto newCourseAssignment)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var alreadyExists = await _caRepo.CourseAssignmentExists(newCourseAssignment.CourseId, newCourseAssignment.FacultyId);
        if (alreadyExists)
            return StatusCode(403, new { message= "The resource already exists" });
        
        var courseAssignment = _mapper.Map<CourseAssignment>(newCourseAssignment);

        var saved = await _caRepo.CreateCourseAssignment(courseAssignment);
        if (!saved)
            return StatusCode(500, new { message = "Oops! Something went wrong" });

        return StatusCode(201, new { Id = courseAssignment.Id });
    }
}