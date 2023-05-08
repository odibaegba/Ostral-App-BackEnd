using AutoMapper;
using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;
using Ostral.Core.Utilities;
using Ostral.Domain.Models;

namespace Ostral.Infrastructure.Repository;

public class TutorCourseRepository : ITutorCourseRepository
{
    private readonly OstralDBContext _context;
    private readonly IMapper _mapper;

    public TutorCourseRepository(OstralDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task AddCourseAsync(Course course)
    {
        await _context.Courses.AddAsync(course);
        await _context.SaveChangesAsync();
    }

    public async Task<PaginatorResponseDTO<IEnumerable<CourseDTO>>> GetCourses(string tutorId, int pageSize = 10, int pageNumber = 1)
    {
        await using var context = _context;

        var courses = _context.Courses
            .Where(course => course.TutorId == tutorId)
            .Select(n => new CourseDTO()
            {
                ImageUrl = n.ImageUrl,
                Name = n.Name
            });

        return await courses.PaginationAsync<CourseDTO, CourseDTO>(pageSize, pageNumber, _mapper);
    }


}