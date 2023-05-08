using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;

namespace Ostral.Core.Results;

public class CategoryCourseResult: IResult
{
    public bool Success { get; set; }
    public PaginatorResponseDTO<IEnumerable<CategoryCourseDTO>> Courses { get; set; } = new();
    public IEnumerable<string> Errors { get; set; } = Array.Empty<string>();
}