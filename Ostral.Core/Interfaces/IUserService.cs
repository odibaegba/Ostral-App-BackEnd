using Ostral.Core.DTOs;
using Ostral.Core.Results;

namespace Ostral.Core.Interfaces;

public interface IUserService
{
    Task<Result<UserDTO>> GetUser(string id);
    Task<Result<UserDTO>> UpdateUserProfile(string id, UpdateUserDTO updateUserDTO);
}