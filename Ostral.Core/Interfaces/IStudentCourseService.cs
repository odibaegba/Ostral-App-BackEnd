using Ostral.Core.DTOs;
using Ostral.Core.Results;

namespace Ostral.Core.Interfaces
{
    public interface IStudentCourseService
    {
        Task<Result<PaginatorResponseDTO<IEnumerable<StudentCourseDTO>>>> GetAllStudentCourses(string id, int pageSize, int pageNumber);
        Task<Result<StudentCourseDTO>> GetStudentCourseById(string studentId, string courseId);
    }
}