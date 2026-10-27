using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Models.RequestModels
{
    public class ReviewRequestModel
    {
        [Required (ErrorMessage ="Enter correct Message")]
        [MaxLength (2000)]
        public string Message { get; set; }
    }
}
