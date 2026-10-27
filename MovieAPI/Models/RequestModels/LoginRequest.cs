using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Models.RequestModels
{
    public class LoginRequest
    {
       
            [Required, EmailAddress]
            public string Email { get; set; }
            [Required]
            public string Password { get; set; }
        
    }
}
