using Ostral.Core.DTOs;

namespace Ostral.Core.Interfaces;

public interface ICategoryCourseRepository
{
    public Task<PaginatorResponseDTO<IEnumerable<CategoryCourseDTO>>> GetAllCategoryCourses(string categoryId, int pageSize, int pageNumber);
}