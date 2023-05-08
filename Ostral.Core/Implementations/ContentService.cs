using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;
using Ostral.Core.Results;

namespace Ostral.Core.Implementations
{
    public class ContentService : IContentService
    {
        private readonly IContentRepository _courseContentRepository;

        public ContentService(IContentRepository courseContentRepository)
        {
            _courseContentRepository = courseContentRepository;
        }

        public async Task<Result<IEnumerable<ContentDTO>>> GetAllCourseContent(string courseId)
        {
            try
            {
                var courseContent = await _courseContentRepository.GetAllCourseContent(courseId);

                if (courseContent == null || courseContent.Count() == 0)
                    return new Result<IEnumerable<ContentDTO>>
                    {
                        Success = false,
                        Errors = new string[] { "No course content found." }
                    };

                return new Result<IEnumerable<ContentDTO>>
                {
                    Success = true,
                    Data = courseContent
                };
            }
            catch (Exception ex) { return new Result<IEnumerable<ContentDTO>>
                {
                    Success = false,
                    Errors = new string[] { ex.Message }
                }; 
            }
        }

        public async Task<Result<ContentDetailedDTO>> GetCourseContent(string contentId)
        {
            try
            {
                var content = await _courseContentRepository.GetCourseContent(contentId);

                if (content == null)
                    return new Result<ContentDetailedDTO>
                    {
                        Success = false,
                        Errors = new string[] { "Courses not found." }
                    };

                return new Result<ContentDetailedDTO>
                {
                    Success = true,
                    Data = content
                };
            }
            catch (Exception ex) { return new Result<ContentDetailedDTO>
                {
                    Success = false,
                    Errors = new string[] { ex.Message }
                }; 
            }
        }
    }
}
