using Ostral.Core.Interfaces;
using Ostral.Core.Results;

namespace Ostral.Core.Implementations;

public class CategoryCourseService: ICategoryCourseService
{
    private readonly ICategoryCourseRepository _categoryCourseRepository;

    public CategoryCourseService(ICategoryCourseRepository categoryCourseRepository)
    {
        _categoryCourseRepository = categoryCourseRepository;
    }

    public async Task<CategoryCourseResult> GetAllCategoryCourses(string categoryId, int pageSize, int pageNumber)
    {
        var courses = await _categoryCourseRepository.GetAllCategoryCourses(categoryId, pageSize, pageNumber);

        return new CategoryCourseResult
        {
            Success = true,
            Courses = courses
        };
    }
}