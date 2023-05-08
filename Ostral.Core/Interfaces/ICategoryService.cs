using Ostral.Core.Results;

namespace Ostral.Core.Interfaces;

public interface ICategoryService
{
    public Task<GetCategoryResult> GetCategoryById(string Id);
    public Task<GetAllCategoriesResult> GetAllCategories();
}