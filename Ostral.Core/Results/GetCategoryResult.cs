using Ostral.Core.Interfaces;
using Ostral.Domain.Models;

namespace Ostral.Core.Results;

public class GetCategoryResult: IResult
{
    public bool Success { get; set; }
    public IEnumerable<string> Errors { get; set; } = Array.Empty<string>();
    public Category Category { get; set; } = new();
}