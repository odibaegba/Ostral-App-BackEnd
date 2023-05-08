using System.Net;
using Microsoft.AspNetCore.Mvc;
using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;

namespace Ostral.API.Controllers;

[ApiController]
[Route("api/search")]
public class SearchController: ControllerBase
{
    private readonly ICourseService _courseService;
    
    public SearchController(ICourseService courseService)
    {
        _courseService = courseService;
    }
    
    [HttpGet("")]
    public async Task<IActionResult> SearchCoursesByKeyword([FromQuery] string keyword, [FromQuery] int pageSize, [FromQuery] int pageNumber)
    {
        if (string.IsNullOrEmpty(keyword))
            return BadRequest(ResponseDTO<object>.Fail(new [] {"Keyword is empty"}, (int) HttpStatusCode.BadRequest));
        
        var result = await _courseService.SearchCourseByKeyword(keyword, pageSize, pageNumber);
        return Ok(ResponseDTO<object>.Success(result.Data!));
    }
}