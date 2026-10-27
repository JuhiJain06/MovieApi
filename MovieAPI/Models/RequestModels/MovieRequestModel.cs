using Microsoft.AspNetCore.Http;
using MovieAPI.Models.DatabaseModels;
using MovieAPI.Repositories;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Models.RequestModels
{
    public class MovieRequestModel
    {
        [Required (ErrorMessage ="enter correct movie name")]
        public string Name { get; set; }


        [Required (ErrorMessage ="year is required")]
        [Range (1800,2100)]
        public int YearOfRelease { get; set; }


        [Required(ErrorMessage ="Plot is required")]
        [MaxLength (1000)]
        public string Plot { get; set; }


        [Required(ErrorMessage ="cover image not uploaded")]
        public IFormFile CoverImage { get; set; }

        [Required(ErrorMessage ="Enter correct Producer Id")]
        public int ProducerId { get; set; }

        [Required(ErrorMessage ="Enter correct Actor Id")]
        public List<int> ActorsId { get; set; }

        [Required (ErrorMessage ="Enter correct Genre Id")]
        public List<int> GenresId { get; set; }

    }
}
