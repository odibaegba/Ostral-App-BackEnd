using AutoMapper;
using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;
using Ostral.Core.Results;

namespace Ostral.Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private readonly OstralDBContext _context;
    private readonly IMapper _mapper;

    public UserRepository(OstralDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<UserDTO>> GetUserByIdAsync(string Id)
    {
        var user = await _context.Users.FindAsync(Id);
        if (user == null)
            return new Result<UserDTO>
            {
                Errors = new List<string> {$"User not found for id: {Id}"}
            };

        var result = _mapper.Map<UserDTO>(user);
        return new Result<UserDTO>
        {
            Success = true,
            Data = result
        };
    }

    public async Task<Result<UserDTO>> UpdateUserProfile(string Id, UpdateUserDTO updateUserDTO)
    {
        var user = await _context.Users.FindAsync(Id);
        if (user == null)
            return new Result<UserDTO> {Errors = new List<string> {$"User not found for id: {Id}"}};

        _mapper.Map(updateUserDTO, user);
        await _context.SaveChangesAsync();
        var result = _mapper.Map<UserDTO>(user);
        return new Result<UserDTO> {Data = result, Success = true};
    }
}