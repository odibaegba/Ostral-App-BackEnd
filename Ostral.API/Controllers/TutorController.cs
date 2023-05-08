using Microsoft.AspNetCore.Mvc;
using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;

namespace Ostral.API.Controllers;

[ApiController]
[Route("api/tutor")]
public class TutorController: ControllerBase
{
    private readonly ITutorService _tutorService;

    public TutorController(ITutorService tutorService)
    {
        _tutorService = tutorService;
    }
    
    [HttpGet("")]
    public async Task<IActionResult> GetTutors([FromQuery] int pageSize, [FromQuery] int pageNumber)
    {
        var response = await _tutorService.GetTutors(pageSize, pageNumber);
        
        return Ok(ResponseDTO<object>.Success(response.Data!));
    }
    
    [HttpGet("popular-tutors")]
    public async Task<IActionResult> GetPopularTutors()
    {
        var response = await _tutorService.GetPopularTutors();
        
        return Ok(ResponseDTO<object>.Success(response.Data!));
    }
}