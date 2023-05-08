using Microsoft.AspNetCore.Mvc;
using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;

namespace Ostral.API.Controllers;

[ApiController]
[Route("api/tutor/{tutorId}/course")]
public class TutorCourseController : ControllerBase
{
    private readonly ITutorCourseService _tutorCourseService;

    public TutorCourseController(ITutorCourseService tutorCourseService)
    {
        _tutorCourseService = tutorCourseService;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetCourses([FromRoute] string tutorId, [FromQuery] int pageSize, [FromQuery] int pageNumber)
    {
        var tutorCourseResult = await _tutorCourseService.GetAllTutorCourses(tutorId, pageSize, pageNumber);
        return Ok(ResponseDTO<object>.Success(tutorCourseResult.Data!));
    }

    [HttpPost("add-course/category/{categoryId}")]
    public async Task<IActionResult> CreateCourse([FromRoute] string tutorId, [FromRoute] string categoryId, CreateCourseDTO course, IFormFile file)
    {
        await _tutorCourseService.AddCourseAsync(course, tutorId, categoryId, file);
        return Ok();
    }
}