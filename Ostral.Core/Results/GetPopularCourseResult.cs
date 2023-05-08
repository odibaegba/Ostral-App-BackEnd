using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;

namespace Ostral.Core.Results
{
    public class GetPopularCourseResult : IResult
    {
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; } = Array.Empty<string>();
        public IEnumerable<CourseDTO> Courses { get; set; } = Array.Empty<CourseDTO>();
    }
}