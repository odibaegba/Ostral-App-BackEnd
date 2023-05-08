using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;

namespace Ostral.Core.DTOs
{
	public class LoginDTO
	{
		[Required, EmailAddress] 
		public string Email { get; set; } = string.Empty;
		
		[Required]
		public string Password { get; set; } = string.Empty;

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
        public string? ReturnUrl { get; set; }= string.Empty;

		public IList<AuthenticationScheme> ExternalLogins { get; set; } = new List<AuthenticationScheme>();
    }
}
