using System;
using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Models.RequestModels
{
    public class ActorRequestModel
    {
        [Required (ErrorMessage="Actor Name cannot be empty")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Actor Bio Cannot be empty")]
        [MaxLength(1000)]
        public string Bio { get; set; }

        [Required(ErrorMessage ="Actor Enter correct Date of birth")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage="Gender cannot be empty")]
        [MaxLength(20)]
        public string Gender { get; set; }
    }
}
