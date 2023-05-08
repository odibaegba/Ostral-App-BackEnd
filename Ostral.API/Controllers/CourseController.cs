using Microsoft.AspNetCore.Mvc;
using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;

namespace Ostral.API.Controllers
{
   
    [ApiController]
    [Route("api/course")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

		[HttpGet("")]
		public async Task<IActionResult> GetAllCourses(int pageSize, int pageNumber)
		{
			var result = await _courseService.GetAllCourses(pageSize, pageNumber);
			if (!result.Success) 
				return NotFound(ResponseDTO<object>.Fail(result.Errors));
			return Ok(ResponseDTO<object>.Success(result.Data!));
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetCourseById([FromRoute]string id)
		{
			var result = await _courseService.GetCourseById(id);
			
			if (!result.Success) 
				return NotFound(ResponseDTO<object>.Fail(result.Errors));
			
			return Ok(ResponseDTO<object>.Success(result.Data!));
		}

		[HttpPost("{courseId}/student/{studentId}")]
		public async Task<IActionResult> EnrollForCourse([FromRoute]string courseId, [FromRoute]string studentId)
		{
			var result = await _courseService.EnrollForCourse(courseId, studentId);

			if (!result.Success) 
				return NotFound(ResponseDTO<object>.Fail(result.Errors));
			
			return Ok(ResponseDTO<object>.Success(result.Data!));
		}        

        [HttpGet("popular-courses")]
        public async Task<IActionResult> GetPopularCourses()
        {
            var courses = await _courseService.GetPopularCourses();
            return Ok(ResponseDTO<object>.Success(courses.Courses));
        }


       
        [HttpGet("random-courses")]
        public async Task<IActionResult> GetRandomCourses()
        {
            var courses = await _courseService.GetRandomCourses();
            
            return Ok(ResponseDTO<object>.Success(courses.Course));
        }
    }
}
