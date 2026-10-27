using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace MovieAPI.Models.RequestModels
{
    public class MovieUpdateRequest
    {
        public string Name { get; set; }
        public int YearOfRelease { get; set; }
        public string Plot { get; set; }
        public IFormFile CoverImage { get; set; }
        public int ProducerId { get; set; }
        public List<int> ActorsId { get; set; }
        public List<int> GenresId { get; set; }
    }
}
