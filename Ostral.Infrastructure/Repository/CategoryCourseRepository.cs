using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;
using Ostral.Core.Utilities;

namespace Ostral.Infrastructure.Repository;

public class CategoryCourseRepository : ICategoryCourseRepository
{
    private readonly OstralDBContext _context;
    private readonly IMapper _mapper;

    public CategoryCourseRepository(OstralDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatorResponseDTO<IEnumerable<CategoryCourseDTO>>> GetAllCategoryCourses(string categoryId, int pageSize = 10,
        int pageNumber = 1)
    {
        var courses = _context.Courses.Where(course => course.CategoryId == categoryId)
            .Include(course => course.Tutor.User)
            .Select(course => new CategoryCourseDTO
            {
                Id = course.Id,
                Name = course.Name,
                Price = course.Price,
                Duration = course.Duration,
                TutorFullName = $"{course.Tutor.User.FirstName} {course.Tutor.User.LastName}",
                ImageUrl = course.ImageUrl,
            });
            
        var paginatedCourses = await courses.PaginationAsync<CategoryCourseDTO, CategoryCourseDTO>(pageSize, pageNumber, _mapper);

        return paginatedCourses;
    }
}