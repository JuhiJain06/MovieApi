using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Models.RequestModels
{
    public class GenreRequestModel
    {
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }
    }
}
