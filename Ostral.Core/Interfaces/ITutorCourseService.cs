using Microsoft.AspNetCore.Http;
using Ostral.Core.DTOs;
using Ostral.Core.Results;

namespace Ostral.Core.Interfaces;

public class TutorCourseResult : Result<PaginatorResponseDTO<IEnumerable<CourseDTO>>>
{
}

public interface ITutorCourseService
{
	public Task<TutorCourseResult> GetAllTutorCourses(string tutorId, int pageSize, int pageNumber);

	public Task AddCourseAsync(CreateCourseDTO data, string tutorId, string categoryId, IFormFile file);
}