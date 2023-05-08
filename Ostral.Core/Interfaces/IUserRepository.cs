using Ostral.Core.DTOs;
using Ostral.Core.Results;

namespace Ostral.Core.Interfaces;

public interface IUserRepository
{
    Task<Result<UserDTO>> GetUserByIdAsync(string Id);
    Task<Result<UserDTO>> UpdateUserProfile(string Id, UpdateUserDTO updateUserDTO);
}