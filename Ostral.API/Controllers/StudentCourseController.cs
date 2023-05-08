using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;
using Ostral.Domain.Models;
using System.Net;

namespace Ostral.API.Controllers
{
	[ApiController]
	[Route("api/student/{studentId}/course")]
	public class StudentCourseController : ControllerBase
	{
		private readonly IStudentCourseService _studentCourseService;

		public StudentCourseController(IStudentCourseService studentCourseService)
		{
			_studentCourseService = studentCourseService;
		}

		[HttpGet("")]
		public async Task<IActionResult> GetAllStudentCourses([FromRoute]string studentId, [FromQuery] int pageSize, [FromQuery] int pageNumber)
		{
			var result = await _studentCourseService.GetAllStudentCourses(studentId, pageSize, pageNumber);
			if (result.Success) return Ok(ResponseDTO<object>.Success(result.Data!));
			return NotFound(ResponseDTO<object>.Fail(new[] { "No courses found." }));
		}

		[HttpGet("{courseId}")]
		public async Task<IActionResult> GetStudentCourse([FromRoute]string studentId, [FromRoute]string courseId)
		{
			var result = await _studentCourseService.GetStudentCourseById(studentId, courseId);
			if (result.Success) return Ok(ResponseDTO<object>.Success(result.Data!));
			return NotFound(ResponseDTO<object>.Fail(new[] { "No courses found." }));
		}
	}
}
