using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ostral.Core.DTOs;
using Ostral.Core.Implementations;
using Ostral.Core.Interfaces;
using Ostral.Domain.Models;
using System.Net;

namespace Ostral.API.Controllers
{
	[ApiController]
	[Route("api/student")]
	public class StudentController : ControllerBase
	{
		private readonly IStudentService _studentService;

		public StudentController(IStudentService studentService)
		{
			_studentService = studentService;
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetStudentById([FromRoute] string id)
		{
			var result = await _studentService.GetStudentById(id);
			if (result.Success) return Ok(ResponseDTO<Student>.Success(result.Data!));
			return NotFound(ResponseDTO<Student>.Fail(new[] { "Student not found." }));
		}
	}
}
