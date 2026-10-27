using System;
using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Models.RequestModels
{
    public class ProducerRequestModel
    {
        [Required(ErrorMessage = "Name cannot be empty")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Bio Cannot be empty")]
        [MaxLength(1000)]
        public string Bio { get; set; }


        [Required(ErrorMessage = "Enter correct Date of birth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [MaxLength(20)]
        public string Gender { get; set; }
    }
}
