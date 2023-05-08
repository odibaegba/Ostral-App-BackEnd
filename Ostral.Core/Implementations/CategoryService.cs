using Ostral.Core.Interfaces;
using Ostral.Core.Results;
using Ostral.Domain.Models;

namespace Ostral.Core.Implementations;

public class CategoryService: ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<GetCategoryResult> GetCategoryById(string id)
    {
        var result = await _categoryRepository.GetCategoryById(id);

        if (result == null)
            return new GetCategoryResult
            {
                Errors = new[] {"No category with this id"}
            };

        return new GetCategoryResult
        {
            Success = true,
            Category = result
        };
    }

    public async Task<GetAllCategoriesResult> GetAllCategories()
    {
        var categories = await _categoryRepository.GetAllCategories();
        var categoriesResult = new List<Category>();
        categoriesResult.AddRange(categories);

        return new GetAllCategoriesResult
        {
            Success = true,
            Categories = categoriesResult
        };
    }
}