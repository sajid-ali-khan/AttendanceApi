using AttendanceApi.Dtos;
using AttendanceApi.Interfaces;
using AttendanceApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SessionController : Controller
{
    private readonly ISessionRepo _sessionRepo;
    private readonly IMapper _mapper;
    private readonly ICourseRepo _courseRepo;
    private readonly IFacultyRepo _facultyRepo;

    public SessionController(ISessionRepo sessionRepo, IMapper mapper, ICourseRepo courseRepo, IFacultyRepo facultyRepo)
    {
        _sessionRepo = sessionRepo;
        _mapper = mapper;
        _courseRepo = courseRepo;
        _facultyRepo = facultyRepo;
    }

    [HttpGet("{sessionId}")]
    [ProducesResponseType(200, Type = typeof(Session))]
    public async Task<IActionResult> GetSessionByIdAsync(int sessionId)
    {
        var session = await _sessionRepo.GetSessionByIdAsync(sessionId);
        if (session is null)
            return NotFound();

        return Ok(session);
    }
    
    [HttpPost]
    [ProducesResponseType(201, Type = typeof(Object))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreateSessionAsync([FromBody]SessionCreateDto newSession)
    {
        var courseExists = await _courseRepo.CourseExists(newSession.CourseId);
        if(!courseExists)
            return BadRequest(new {message = $"A course with courseId = {newSession.CourseId} doesn't exist."});

        var facultyExists = await _facultyRepo.FacultyExists(newSession.FacultyId);
        if(!facultyExists)
            return BadRequest(new {message = $"A faculty with facultyId = {newSession.FacultyId} doesn't exist."});
        
        var studentCount = await _courseRepo.StudentCount(newSession.CourseId);

        var session = new Session
        {
            CourseId = newSession.CourseId,
            FacultyId = newSession.FacultyId,
            NumAbsent = studentCount,
            NumPresent = 0
        };
        
        bool saved = await _sessionRepo.CreateSessionAsync(session);

        if (!saved)
            return StatusCode(500, new { message = "Something went wrong" });

        return Created();
    }
}
