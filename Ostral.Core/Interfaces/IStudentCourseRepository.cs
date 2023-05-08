using Ostral.Core.DTOs;

namespace Ostral.Core.Interfaces
{
	public interface IStudentCourseRepository
	{
		public Task<PaginatorResponseDTO<IEnumerable<StudentCourseDTO>>> GetAllStudentCourses(string id, int pageSize, int pageNumber);
		public Task<StudentCourseDTO?> GetStudentCourse(string studentId, string courseId);
		public bool Add(string studentId, string courseId, out string err);
	}
}