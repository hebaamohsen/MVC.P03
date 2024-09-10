using System.ComponentModel.DataAnnotations;

namespace hebamohsen_MVC_P03.Models
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage ="First Name Is Required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name Is Required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage ="Invalid Format For Email")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Password is required")]

        [RegularExpression("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[\\W_])(.)(?!\\1{2,}).{6,}$")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare(nameof(Password),ErrorMessage ="Confirm Password does not match Password")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage ="Required To Agree")]
        public bool IsAgree { get; set; }



    }
}
