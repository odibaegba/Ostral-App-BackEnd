using Ostral.Core.Results;

namespace Ostral.Core.Interfaces;

public interface ICategoryCourseService
{
    public Task<CategoryCourseResult> GetAllCategoryCourses(string categoryId, int pageSize, int pageNumber);
}