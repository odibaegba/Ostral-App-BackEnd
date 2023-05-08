
using Ostral.Core.DTOs;

namespace Ostral.Core.Interfaces
{
	public interface IContentRepository
	{
		Task<IEnumerable<ContentDTO>> GetAllCourseContent(string courseId);
		Task<ContentDetailedDTO> GetCourseContent(string contentId);
	}
}
