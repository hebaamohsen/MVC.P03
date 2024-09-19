using System.ComponentModel.DataAnnotations;

namespace hebamohsen_MVC_P03.Models
{
	public class ForgetPasswordViewModel
	{
		[Required(ErrorMessage = "Email Is Required")]
		[EmailAddress(ErrorMessage = "Invalid Format For Email")]
		public string Email { get; set; }
	}
}
