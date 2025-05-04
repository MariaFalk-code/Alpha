using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.ViewModels
{
    public class RegisterFormModel
    {
        [Required(ErrorMessage = "This field is required.")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "This field is required.")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "This field is required.")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email address.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "You must enter a password")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$",
        ErrorMessage = "Password must be at least 8 characters long, contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]

        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "You must confirm your password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The passwords do not match.")]
        public string ConfirmPassword { get; set; } = null!;

        [Required(ErrorMessage = "You must accept terms and conditions to continue.")]
        public bool TermsAccepted { get; set; }
    }
}
