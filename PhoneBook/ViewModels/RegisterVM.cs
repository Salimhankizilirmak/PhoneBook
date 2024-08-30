using System.ComponentModel.DataAnnotations;

namespace PhoneBook.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "EMail is required")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; } = "";
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; } = "";
        [Compare("Password", ErrorMessage="Password don't Match.")]
        public string ConfirmPassword { get; set; } = "";
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
