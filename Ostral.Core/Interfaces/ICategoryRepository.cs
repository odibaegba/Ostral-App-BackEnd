using Ostral.Domain.Models;

namespace Ostral.Core.Interfaces;

public interface ICategoryRepository
{
    public Task<Category?> GetCategoryById(string Id);
    public Task<IEnumerable<Category>> GetAllCategories();
}