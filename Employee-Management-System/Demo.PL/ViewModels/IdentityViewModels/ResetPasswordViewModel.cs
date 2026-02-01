using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels.IdentityViewModels
{
    public class ResetPasswordViewModel
    {

        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = "Confrim Password is Required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password Not Match")]
        public string ConfirmPassword { get; set; }
    }
}
