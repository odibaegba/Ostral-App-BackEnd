using Ostral.Domain.Enums;

namespace Ostral.Core.DTOs
{
	public class ContentDetailedDTO : ContentDTO
	{
        public ContentMediaType Type { get; set; }
        public decimal Percentage { get; set; }
        public string Url { get; set; } = string.Empty;
        public string PublicId { get; set; } = string.Empty;
        public DateTime? Completed { get; set; }

        public string CourseId { get; set; } = string.Empty;
    }
}
