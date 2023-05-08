namespace Ostral.Core.Interfaces;

public interface IResult
{
    public bool Success { get; set; }
    public IEnumerable<string> Errors { get; set; }
}