using System.Collections.Generic;

namespace MovieAPI.Models.DTOs
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int YearOfRelease { get; set; }
        public string Plot { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public int ProducerId { get; set; }
        public string ActorIds { get; set; } 
        public string GenreIds { get; set; } 
    }
}
