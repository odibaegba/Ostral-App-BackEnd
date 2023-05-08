using Ostral.Core.Interfaces;

namespace Ostral.Core.Results;

public class AuthenticationResult: IResult
{
    public bool Success { get; set; }

    public string Token { get; set; } = string.Empty;

    public string RefreshToken { get; set; } = string.Empty;
    public IEnumerable<string> Errors { get; set; } = Array.Empty<string>();

}