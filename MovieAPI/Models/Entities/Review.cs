using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieAPI.Models.DatabaseModels
{
    public class Review 
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int MovieId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
