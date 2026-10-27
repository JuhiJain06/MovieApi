using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Models.RequestModels
{
    public class SignUpRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        
    }
}
