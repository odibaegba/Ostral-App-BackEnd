using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;

namespace Ostral.API.Controllers
{
	[ApiController]
	[Route("api/content")]
	public class ContentController : ControllerBase
	{
		private readonly IContentService _courseContentService;

		public ContentController(IContentService courseContentService)
		{
			_courseContentService = courseContentService;
		}

		[HttpGet("{contentId}")]
		public async Task<IActionResult> GetCourseContent([FromRoute] string contentId)
		{
			var result = await _courseContentService.GetCourseContent(contentId);
			if (!result.Success)
				return NotFound(result);
			
			return Ok(ResponseDTO<object>.Success(result.Data));
		}
	}
}
