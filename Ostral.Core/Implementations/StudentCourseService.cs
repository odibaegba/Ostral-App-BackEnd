using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;
using Ostral.Core.Results;
using Ostral.Domain.Models;

namespace Ostral.Core.Implementations
{
	public class StudentCourseService : IStudentCourseService
	{
		private readonly IStudentCourseRepository _studentCourseRepository;

		public StudentCourseService(IStudentCourseRepository studentCourseRepository)
		{
			_studentCourseRepository = studentCourseRepository;
		}

		public async Task<Result<PaginatorResponseDTO<IEnumerable<StudentCourseDTO>>>> GetAllStudentCourses(string id, int pageSize, int pageNumber)
		{
			var courses = await _studentCourseRepository.GetAllStudentCourses(id, pageSize, pageNumber);

			return new Result<PaginatorResponseDTO<IEnumerable<StudentCourseDTO>>>
			{
				Success = true,
				Data = courses
			};
		}

		public async Task<Result<StudentCourseDTO>> GetStudentCourseById(string studentId, string courseId)
		{
			var course = await _studentCourseRepository.GetStudentCourse(studentId, courseId);
			
			return course == null 
				? new Result<StudentCourseDTO> {Success = false, Errors = new []{"course not found"}} 
				: new Result<StudentCourseDTO> { Success = true, Data = course };
		}
	}
}
