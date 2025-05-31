using AttendanceApi.Dtos;
using AttendanceApi.Interfaces;
using AttendanceApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SchemeController : Controller
{
    private readonly ISchemeRepo _schemeRepo;
    private readonly IMapper _mapper;

    public SchemeController(ISchemeRepo schemeRepo, IMapper mapper)
    {
        _schemeRepo = schemeRepo;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<SchemeOutputDto>))]
    public async Task<IActionResult> GetSchemes()
    {
        var schemeEntities = await _schemeRepo.GetSchemes();
        var schemes = _mapper.Map<IEnumerable<SchemeOutputDto>>(schemeEntities);
        return Ok(schemes);
    }

    [HttpGet("{schemeId}/offeredPrograms")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<OfferedProgram>))]
    public async Task<IActionResult> GetOfferedPrograms(int schemeId)
    {
        var offeredProgramEntities = await _schemeRepo.GetOfferedPrograms(schemeId);
        var offeredProgramDtos = _mapper.Map<IEnumerable<OfferedProgramsOutputDto>>(offeredProgramEntities);
        return Ok(offeredProgramDtos);
    }
}