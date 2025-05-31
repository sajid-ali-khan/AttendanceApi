using AttendanceApi.Dtos;
using AttendanceApi.Interfaces;
using AttendanceApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OfferedProgramController: Controller
{
    private readonly IOfferedProgramRepo _offeredProgramRepo;
    private readonly IMapper _mapper;

    public OfferedProgramController(IOfferedProgramRepo repo, IMapper mapper)
    {
        _offeredProgramRepo = repo;
        _mapper = mapper;
    }

    [HttpGet("{schemeId}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<OfferedProgram>))]
    public async Task<IActionResult> GetOfferedProgramsForScheme(int schemeId)
    {
        var offeredProgramEntities = await _offeredProgramRepo.GetOfferedProgramsForScheme(schemeId);
        var offeredProgramDtos = _mapper.Map<IEnumerable<OfferedProgramsOutputDto>>(offeredProgramEntities);
        return Ok(offeredProgramDtos);
    }
}