namespace Ostral.Core.DTOs;

public class StudentCourseDTO
{
    public string CourseId { get; set; } = string.Empty;
    public string CourseName { get; set; } = string.Empty;
    public string CourseImageUrl { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public int Duration { get; set; }
    public int ContentCount { get; set; }
    public int CompletionPercentage { get; set; }
    public DateTime? CompletionDate { get; set; }
}