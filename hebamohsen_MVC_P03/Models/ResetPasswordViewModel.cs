using System.ComponentModel.DataAnnotations;

namespace hebamohsen_MVC_P03.Models
{
	public class ResetPasswordViewModel
	{
		[Required(ErrorMessage = "Password is required")]

		[RegularExpression("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[\\W_])(.)(?!\\1{2,}).{6,}$", ErrorMessage = "incorrect password")]

		public string Password { get; set; }
		[Required(ErrorMessage = "Confirm Password is required")]
		[Compare(nameof(Password), ErrorMessage = "Confirm Password does not match Password")]
		public string ConfirmPassword { get; set; }
		public string Email { get; set; }
		public string Token { get; set; }
	}
}
