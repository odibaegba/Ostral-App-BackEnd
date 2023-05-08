using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;
using Ostral.Core.Results;
using Ostral.Domain.Models;

namespace Ostral.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly IIdentityService _identityService;
        private readonly ILogger<AuthController> _logger;
        private readonly SignInManager<User> _signInManager;
        public AuthController(IIdentityService identityService, ILogger<AuthController> logger, SignInManager<User> signInManager)
        {
            _identityService = identityService;
            _logger = logger;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginWithEmailAndPassword([FromBody] LoginDTO model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage));
                return BadRequest(ResponseDTO<object>.Fail(errors, (int) HttpStatusCode.BadRequest));
            }

            var response = await _identityService.LoginWithEmailAndPassword(model);

            if (!response.Success)
                return BadRequest(
                    ResponseDTO<AuthenticationResult>.Fail(response.Errors, (int) HttpStatusCode.BadRequest));

            return Ok(ResponseDTO<object>.Success(new
            {
                response.Token,
                response.RefreshToken
            }));
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterStudent([FromBody] RegisterDTO register)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage));
                return BadRequest(ResponseDTO<object>.Fail(errors, (int) HttpStatusCode.BadRequest));
            }

            var response = await _identityService.RegisterStudent(register);

            if (!response.Success)
                return BadRequest(
                    ResponseDTO<AuthenticationResult>.Fail(response.Errors, (int) HttpStatusCode.BadRequest));

            return Ok(ResponseDTO<object>.Success(new
            {
                response.Token,
                response.RefreshToken
            }, "", (int) HttpStatusCode.Created));
        }

        [HttpPost("register-tutor")]
        public async Task<IActionResult> RegisterTutor([FromForm] RegisterDTO register)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage));
                return BadRequest(ResponseDTO<object>.Fail(errors, (int) HttpStatusCode.BadRequest));
            }

            var response = await _identityService.RegisterTutor(register);

            if (!response.Success)
                return BadRequest(
                    ResponseDTO<object>.Fail(response.Errors, (int) HttpStatusCode.BadRequest));

            return Ok(ResponseDTO<object>.Success(new
            {
                response.Token,
                response.RefreshToken
            }, "", (int) HttpStatusCode.Created));
        }
        
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string? returnUrl)
        {
            LoginDTO model = new LoginDTO
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            return Ok(ResponseDTO<object>.Success(model));
        }
        
        [AllowAnonymous]
        [HttpPost]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            // var redirectUrl = Url.Action("ExternalLoginCallback", "Account",
            //                         new { ReturnUrl = returnUrl });

            var properties =
                _signInManager.ConfigureExternalAuthenticationProperties(provider, returnUrl);

            return Ok(ResponseDTO<object>.Success(new ChallengeResult(provider, properties)));
        }
    }
}