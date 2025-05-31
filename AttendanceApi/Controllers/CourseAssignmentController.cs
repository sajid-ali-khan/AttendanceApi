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
    [ProducesResponseType(200, Type = typeof(IEnumerable<CourseAssignment>))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetCourseAssignment(int courseAssignmentId)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var exists = await _caRepo.CourseAssignmentExists(courseAssignmentId);
        if (!exists)
            return NotFound();
        var courseAssignment = await _caRepo.GetCourseAssignment(courseAssignmentId);
        return Ok(courseAssignment);
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

        return CreatedAtAction(
            nameof(GetCourseAssignment),
            new { Id = courseAssignment.Id },
            newCourseAssignment
        );
    }
}