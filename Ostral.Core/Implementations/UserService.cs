using AutoMapper;
using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;
using Ostral.Core.Results;
using Ostral.Domain.Models;

namespace Ostral.Core.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task<Result<UserDTO>> GetUser(string id)
    {
        return await _userRepository.GetUserByIdAsync(id);
    }

    public async Task<Result<UserDTO>> UpdateUserProfile(string id, UpdateUserDTO updateUserDTO)
    {
        return await _userRepository.UpdateUserProfile(id, updateUserDTO);
    }
}