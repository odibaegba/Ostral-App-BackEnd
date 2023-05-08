using Ostral.Core.DTOs;
using Ostral.Core.Results;

namespace Ostral.Core.Interfaces
{
	public interface ICourseService
	{
		Task<Result<PaginatorResponseDTO<IEnumerable<CourseDTO>>>> GetAllCourses(int pageSize, int pageNumber);

		Task<Result<CourseDetailedDTO>> GetCourseById(string id);

		Task<Result<CourseDTO>> EnrollForCourse(string courseId, string studentId);
        Task<GetRandomCourseResult> GetRandomCourses();
        Task<GetPopularCourseResult> GetPopularCourses();

        Task<Result<PaginatorResponseDTO<IEnumerable<CourseDTO>>>> SearchCourseByKeyword(string keyword, int pageSize, int pageNumber);
	}
}
 