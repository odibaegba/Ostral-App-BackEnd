using Microsoft.AspNetCore.Mvc;
using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;

namespace Ostral.API.Controllers;

[ApiController]
[Route("api/category")]
public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById([FromRoute] string id)
    {
        var categoryResult = await _categoryService.GetCategoryById(id);
        if (!categoryResult.Success)
            return NotFound(ResponseDTO<object>.Fail(categoryResult.Errors));

        return Ok(ResponseDTO<object>.Success(categoryResult.Category));
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAllCategories()
    {
        var result = await _categoryService.GetAllCategories();

        return Ok(ResponseDTO<object>.Success(result.Categories));
    }
}