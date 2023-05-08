using Ostral.Core.Interfaces;
using Ostral.Domain.Models;

namespace Ostral.Core.Results;

public class GetAllCategoriesResult: IResult
{
    public bool Success { get; set; }
    public IEnumerable<Category> Categories { get; set; } = Array.Empty<Category>();
    public IEnumerable<string> Errors { get; set; } = Array.Empty<string>();
}