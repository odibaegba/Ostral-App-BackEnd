
using Microsoft.EntityFrameworkCore;
using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;

namespace Ostral.Infrastructure.Repository
{
    public class ContentRespository : IContentRepository
    {
        private readonly OstralDBContext _context;

        public ContentRespository(OstralDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ContentDTO>> GetAllCourseContent(string courseId)
        {
            var contents = await _context.Contents
                .Where(c => courseId == c.CourseId)
                .Select(c => new ContentDTO
                {
                    Id = c.Id,
                    Title = c.Title,
                    Duration = c.Duration,
                    IsDownloadable = c.IsDownloadable,
                }).ToListAsync();

            return contents;
        }

        public async Task<ContentDetailedDTO> GetCourseContent(string contentId)
        {
            var course = await _context.Contents
                .Where(c => contentId == c.Id)
                .Select(c => new ContentDetailedDTO
                {
                    Id = c.Id,
                    Title = c.Title,
                    Duration = c.Duration,
                    CourseId = c.CourseId,
                    Completed = c.Completed,
                    IsDownloadable = c.IsDownloadable,
                    Percentage = c.Percentage,
                    PublicId = c.PublicId,
                    Type = c.Type,
                    Url = c.Url
                }).FirstOrDefaultAsync();

            return course!;
        }
    }
}
