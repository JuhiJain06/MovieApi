using System;
using System.ComponentModel.DataAnnotations;
namespace MovieAPI.Models.DatabaseModels
{
    public class Actor 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
