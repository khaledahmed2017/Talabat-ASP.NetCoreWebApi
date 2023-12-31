using System.ComponentModel.DataAnnotations;

namespace Talabat.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        [RegularExpression("^(?:(?=.*[a-z])(?:(?=.*[A-Z])(?=.*[\\d\\W])|(?=.*\\W)(?=.*\\d))|(?=.*\\W)(?=.*[A-Z])(?=.*\\d)).{8,}$",ErrorMessage = "At least one Upper case letter,At least Lower case,At least one Numbers,Disallow the consecutive digits like 1234, 4567, etc,Disallow the consecutive alphabets like abcd, ijkl, etc")]
        public string Password { get; set; }
    }
}
