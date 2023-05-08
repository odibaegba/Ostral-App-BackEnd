using Microsoft.EntityFrameworkCore;
using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;
using AutoMapper;
using Ostral.Core.Utilities;
using Ostral.Domain.Models;

namespace Ostral.Infrastructure.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly OstralDBContext _context;
        private readonly IMapper _mapper;

        public CourseRepository(OstralDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatorResponseDTO<IEnumerable<CourseDTO>>> GetAllCourses(int pageSize = 10,
            int pageNumber = 1)
        {
            var courses = _context.Courses
                .Select(c => new CourseDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    TutorFullName = $"{c.Tutor.User.FirstName} {c.Tutor.User.LastName}",
                    Duration = c.Duration,
                    ImageUrl = c.ImageUrl,
                    Price = c.Price,
                    StudentCount = c.StudentList.Count,
                    ContentCount = c.ContentList.Count
                });

            return await courses.PaginationAsync<CourseDTO, CourseDTO>(pageSize, pageNumber, _mapper);
        }

        public async Task<CourseDetailedDTO> GetCourseById(string courseID)
        {
            await using (_context)
            {
                var result = await _context.Courses
                    .Where(c => courseID == c.Id)
                    .Select(c => new CourseDetailedDTO
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Description = c.Description,
                        TutorFullName = $"{c.Tutor.User.FirstName} {c.Tutor.User.LastName}",
                        TutorDescription = c.Tutor.Profession,
                        TutorId = c.TutorId,
                        TutorImageUrl = c.Tutor.User.ImageUrl,
                        ImageUrl = c.ImageUrl,
                        Price = c.Price,
                        Duration = c.Duration,
                        Completed = c.Completed,
                        CategoryId = c.CategoryId,
                        CategoryName = c.Category.Name,
                        ContentList = c.ContentList.Select(c1 => new ContentDTO
                        {
                            Id = c1.Id,
                            Duration = c1.Duration,
                            IsDownloadable = c1.IsDownloadable,
                            Title = c1.Title
                        }).ToList(),
                        StudentCount = c.StudentList.Count(),
                        ContentCount = c.ContentList.Count
                    })
                    .FirstOrDefaultAsync(c => courseID == c.Id);
                return result!;
            }
        }

        public async Task<CourseDTO> UpdateCourse(Course course, string id)
        {
            var courseToUpdate = await GetCourseById(course.Id);
            if (courseToUpdate == null) return courseToUpdate!;

            await using (_context)
            {
                _context.Courses.Update(course);
                await _context.SaveChangesAsync();
                return courseToUpdate;
            }
        }

        public async Task<ICollection<CourseDTO>> GetPopularCourses()
        {
            var popularCourses = await _context.Courses.OrderByDescending(c => c.StudentList.Count)
                .Select(c => new CourseDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    TutorFullName = $"{c.Tutor.User.FirstName} {c.Tutor.User.LastName}",
                    Duration = c.Duration,
                    ImageUrl = c.ImageUrl,
                    Price = c.Price
                })
                .Take(6)
                .ToListAsync();

            return popularCourses;
        }

        public async Task<Course> GetRandomCourse()
        {
            var randomCourses = await _context.Courses
                .OrderBy(c => EF.Functions.Random()).FirstOrDefaultAsync();

            return randomCourses!;
        }

        public async Task<PaginatorResponseDTO<IEnumerable<CourseDTO>>> SearchCourseByKeyword(string keyword,
            int pageSize = 10, int pageNumber = 1)
        {
            keyword = keyword.ToLower();
            var courses = _context.Courses
                .Where(c => c.Category.Name.ToLower().Contains(keyword) || c.Name.ToLower().Contains(keyword))
                .Select(c => new CourseDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    TutorFullName = $"{c.Tutor.User.FirstName} {c.Tutor.User.LastName}",
                    Duration = c.Duration,
                    ImageUrl = c.ImageUrl,
                    Price = c.Price,
                    StudentCount = c.StudentList.Count,
                    ContentCount = c.ContentList.Count
                });

            return await courses.PaginationAsync<CourseDTO, CourseDTO>(pageSize, pageNumber, _mapper);
        }
    }
}