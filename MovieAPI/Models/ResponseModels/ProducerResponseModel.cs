using System;

namespace MovieAPI.Models.ResponseModels
{
    public class ProducerResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
    }
}
