using System;
using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Models.DatabaseModels
{
    public class Genre 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
