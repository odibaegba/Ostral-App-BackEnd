using Ostral.Core.DTOs;
using Ostral.Core.Results;

namespace Ostral.Core.Interfaces
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> LoginWithEmailAndPassword(LoginDTO model);
        Task<AuthenticationResult> RegisterStudent(RegisterDTO user);
        Task<AuthenticationResult> RegisterTutor(RegisterDTO user);
    }
}
