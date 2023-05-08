using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;
using Ostral.Core.Results;

namespace Ostral.Core.Implementations
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IStudentCourseRepository _studentCourseRepository;

		public CourseService(ICourseRepository courseRepository, IStudentCourseRepository studentCourseRepository)
		{
			_courseRepository = courseRepository;
			_studentCourseRepository = studentCourseRepository;
		}

		public async Task<Result<PaginatorResponseDTO<IEnumerable<CourseDTO>>>> GetAllCourses(int pageSize, int pageNumber)
		{
            try
            {
                var paginatedResponse = await _courseRepository.GetAllCourses(pageSize, pageNumber);
                return new Result<PaginatorResponseDTO<IEnumerable<CourseDTO>>>
                {
                    Data = paginatedResponse,
                    Success = true
                };
            }
            catch (Exception ex) {
                return new Result<PaginatorResponseDTO<IEnumerable<CourseDTO>>>
                {
                    Success = false,
                    Errors = new string[] { ex.Message }
                };
            }
		}

		public async Task<Result<CourseDetailedDTO>> GetCourseById(string id)
		{
			try
			{
                var course = await _courseRepository.GetCourseById(id);
                if (course == null) return new Result<CourseDetailedDTO>
                {
                    Success = false,
                    Errors = new string[] { "Course not found." }
                };

                return new Result<CourseDetailedDTO>
                {
                    Data = course,
                    Success = true
                };
            }
            catch(Exception ex) { return new Result<CourseDetailedDTO>
                {
                    Success = false,
                    Errors = (new string[] { ex.Message })
                };
            }
		}

		public async Task<Result<CourseDTO>> EnrollForCourse(string courseId, string studentId)
		{
            try
            {
                var result = _studentCourseRepository.Add(studentId, courseId, out var errMsg);
                if (!result) return new Result<CourseDTO>
                {
                    Success = false,
                    Errors = new string[] { "Unable to enroll for a course." }
                };

                return new Result<CourseDTO>
                {
                    Data = await _courseRepository.GetCourseById(courseId),
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new Result<CourseDTO>
                {
                    Success = false,
                    Errors = new string[] { ex.Message }
                };
            }
		}
		
        public async Task<GetPopularCourseResult> GetPopularCourses()
        {

          var  popularCourses = await _courseRepository.GetPopularCourses();
            if (popularCourses == null || popularCourses.Count == 0) return new GetPopularCourseResult
            {
                Success = false,
                Errors = new List<string> { "Popular Courses not found" }
            };

            return new GetPopularCourseResult { Success = true, Courses = popularCourses };
        }

        public async Task<GetRandomCourseResult> GetRandomCourses()
        {
            var randomCourses = await _courseRepository.GetRandomCourse();
            if (randomCourses == null) return new GetRandomCourseResult
            {
                Success = false,
                Errors = new List<string> { " Course not found" }
            };

            return new GetRandomCourseResult { Success = true, Course = randomCourses };
        }

        public async Task<Result<PaginatorResponseDTO<IEnumerable<CourseDTO>>>> SearchCourseByKeyword(string keyword,
            int pageSize, int pageNumber)
        {
            var result = await _courseRepository.SearchCourseByKeyword(keyword, pageSize, pageNumber);
            return new Result<PaginatorResponseDTO<IEnumerable<CourseDTO>>>
            {
                Data = result,
                Success = true,
            };
        }
    }
}
