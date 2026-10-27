using System;

namespace MovieAPI.Models.RequestModels
{
    public class ProducerUpdateRequest
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Bio { get; set; }
        public string Gender { get; set; }
    }
}
