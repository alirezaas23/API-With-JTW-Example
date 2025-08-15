using System.ComponentModel.DataAnnotations;

namespace JWTTraining.Dtos.Auth
{
    public class AuthenticationRequestDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
