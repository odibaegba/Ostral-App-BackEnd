using Ostral.Core.DTOs;
using Ostral.Domain.Models;

namespace Ostral.Core.Interfaces;

public interface ITutorCourseRepository
{
    public Task<PaginatorResponseDTO<IEnumerable<CourseDTO>>> GetCourses(string tutorId, int pageSize = 10, int pageNumber = 1);

    public Task AddCourseAsync(Course course);
}