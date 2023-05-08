using Microsoft.AspNetCore.Mvc;
using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;

namespace Ostral.API.Controllers;

[ApiController]
[Route("/api/category/{categoryId}/course")]
public class CategoryCourseController : ControllerBase
{
    private readonly ICategoryCourseService _categoryCourseService;

    public CategoryCourseController(ICategoryCourseService categoryCourseService)
    {
        _categoryCourseService = categoryCourseService;
    }
    
    [HttpGet("")]
    public async Task<IActionResult> GetAllCategoryCourses([FromRoute] string categoryId, [FromQuery] int pageSize,
        [FromQuery] int pageNumber)
    {
        var categoryCourses = await _categoryCourseService.GetAllCategoryCourses(categoryId, pageSize, pageNumber);
        if (!categoryCourses.Success)
            return NotFound(ResponseDTO<object>.Fail(categoryCourses.Errors));

        return Ok(ResponseDTO<PaginatorResponseDTO<IEnumerable<CategoryCourseDTO>>>.Success(categoryCourses.Courses));
    }
}