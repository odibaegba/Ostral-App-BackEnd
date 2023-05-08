using Microsoft.EntityFrameworkCore;
using Ostral.Core.Interfaces;
using Ostral.Domain.Models;

namespace Ostral.Infrastructure.Repository;

public class CategoryRepository: ICategoryRepository
{
    private readonly OstralDBContext _context;

    public CategoryRepository(OstralDBContext context)
    {
        _context = context;
    }
    
    public async Task<Category?> GetCategoryById(string Id)
    {
        return await _context.Categories.FindAsync(Id);
    }

    public async Task<IEnumerable<Category>> GetAllCategories()
    {
        return await _context.Categories.ToListAsync();
    }
}