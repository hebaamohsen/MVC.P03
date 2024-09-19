using System.ComponentModel.DataAnnotations;

namespace hebamohsen_MVC_P03.Models
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "Email Is Required")]
		[EmailAddress(ErrorMessage = "Invalid Format For Email")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Password is required")]

		public string Password { get; set; }
		public bool RememberMe { get; set; }
	}
}
