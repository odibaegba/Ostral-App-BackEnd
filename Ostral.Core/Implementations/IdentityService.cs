using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;
using Ostral.Core.Results;
using Ostral.Domain.Enums;
using Ostral.Domain.Models;

namespace Ostral.Core.Implementations
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> _userManager;
        private readonly IStudentRepository _studentRepository;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityService(UserManager<User> userManager,IStudentRepository studentRepository, IJwtTokenService jwtTokenService, IMapper mapper,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _studentRepository = studentRepository;
            _jwtTokenService = jwtTokenService;
            _mapper = mapper;
            _roleManager = roleManager;
        }
        
        public async Task<AuthenticationResult> LoginWithEmailAndPassword(LoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                return new AuthenticationResult
                {
                    Success = false,
                    Errors = new[] {"Invalid login credentials"}
                };

            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!userHasValidPassword)
                return new AuthenticationResult
                {
                    Success = false,
                    Errors = new[] {"Invalid login credentials"}
                };

            return await GenerateAuthenticationResult(user);
        }

        public async Task<AuthenticationResult> RegisterStudent(RegisterDTO register)
        {
            var users = _mapper.Map<User>(register);
            var existingUser = await _userManager.FindByEmailAsync(users.Email!);
            if (existingUser != null)
                return new AuthenticationResult
                {
                    Errors = new[] {"User with this email already exists"}
                };

            var user = new User
            {
                FirstName = register.FirstName,
                LastName = register.LastName,
                Email = register.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                PhoneNumber = register.PhoneNumber,
                UserName = register.Email,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            var result = await _userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
                return new AuthenticationResult
                {
                    Errors = new[] {$"Failed to create user: {string.Join(",", result.Errors)}"}
                };

            var student = new Student
            {
                UserId = user.Id,
            };

            var resp = await _studentRepository.AddStudent(student);
            if (!resp)
                return new AuthenticationResult {Errors = new[] {"Failed to create student"}};

            if (!await _roleManager.RoleExistsAsync(UserRole.Student.ToString()))
                await _roleManager.CreateAsync(new IdentityRole(UserRole.Student.ToString()));
            if (await _roleManager.RoleExistsAsync(UserRole.Student.ToString()))
            {
                await _userManager.AddToRoleAsync(user, UserRole.Student.ToString());
            }

            return await GenerateAuthenticationResult(user);
        }

        public async Task<AuthenticationResult> RegisterTutor(RegisterDTO register)
        {
            var users = _mapper.Map<User>(register);
            var existingUser = await _userManager.FindByEmailAsync(users.Email!);
            
            if (existingUser != null)
                return new AuthenticationResult
                {
                    Errors = new[] {"User with this email already exists"}
                };

            var user = new User
            {
                FirstName = register.FirstName,
                LastName = register.LastName,
                Email = register.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                PhoneNumber = register.PhoneNumber,
                UserName = register.Email,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            var result = await _userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
                return new AuthenticationResult
                {
                    Errors = new[] {$"Failed to create user: {string.Join(",", result.Errors)}"}
                };

            if (!await _roleManager.RoleExistsAsync(UserRole.Tutor.ToString()))
                await _roleManager.CreateAsync(new IdentityRole(UserRole.Tutor.ToString()));

            if (await _roleManager.RoleExistsAsync(UserRole.Tutor.ToString()))
            {
                await _userManager.AddToRoleAsync(user, UserRole.Tutor.ToString());
            }

            return await GenerateAuthenticationResult(user);
        }

        private async Task<AuthenticationResult> GenerateAuthenticationResult(User user)
        {
            var token = await _jwtTokenService.GenerateAccessToken(user);
            var refreshToken = _jwtTokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMonths(6);
            
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return new AuthenticationResult
                {
                    Errors = new[] {$"Failed to update user's refresh token: {string.Join(",", result.Errors)}"}
                };

            return new AuthenticationResult
            {
                Success = true,
                Token = token,
                RefreshToken = refreshToken
            };
        }
    }
}