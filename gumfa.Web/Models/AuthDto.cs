using System.ComponentModel.DataAnnotations;

namespace gumfa.Web.Models
{
    public class LoginRequestDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class LoginResponseDto
    {
        public UserDto User { get; set; }
        public string Token { get; set; }
    }

    public class UserDto
    {
        public string ID { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class RegistrationRequestDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string EmpID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
        public string? Role { get; set; }
    }
}
