using Ostral.Core.DTOs;
using Ostral.Domain.Models;

namespace Ostral.Core.Interfaces
{
    public interface ICourseRepository
    {
        Task<PaginatorResponseDTO<IEnumerable<CourseDTO>>> GetAllCourses(int pageSize = 10, int pageNumber = 1);

        Task<CourseDetailedDTO> GetCourseById(string courseID);

        Task<CourseDTO> UpdateCourse(Course course, string id);
        Task<ICollection<CourseDTO>> GetPopularCourses();
        Task<Course> GetRandomCourse();
        Task<PaginatorResponseDTO<IEnumerable<CourseDTO>>> SearchCourseByKeyword(string keyword, int pageSize = 10, int pageNumber = 1);
    }
}