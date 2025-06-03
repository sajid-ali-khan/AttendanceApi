using AttendanceApi.Dtos;
using AttendanceApi.Interfaces;
using AttendanceApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FacultyController : Controller
{
    private IFacultyRepo _facultyRepo;
    private readonly IMapper _mapper;

    public FacultyController(IFacultyRepo facultyRepo, IMapper mapper)
    {
        _facultyRepo = facultyRepo;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<FacultyOutputDto>))]
    public async Task<IActionResult> GetFaculties()
    {
        var facultyEntities = await _facultyRepo.GetFaculties();
        var facultyDtos = _mapper.Map<FacultyOutputDto[]>(facultyEntities);
        return Ok(facultyDtos);
    }

    [HttpGet("{facultyId}")]
    [ProducesResponseType(200, Type = typeof(FacultyOutputDtoSingle))]
    public async Task<IActionResult> GetFaculty(int facultyId)
    {
        var facultyEntity = await _facultyRepo.GetFacultyById(facultyId);
        var facultyDto = _mapper.Map<FacultyOutputDtoSingle>(facultyEntity);
        return Ok(facultyDto);
    }
}