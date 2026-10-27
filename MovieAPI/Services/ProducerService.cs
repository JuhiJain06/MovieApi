using MovieAPI.Exceptions;
using MovieAPI.Models.DatabaseModels;
using MovieAPI.Models.RequestModels;
using MovieAPI.Models.ResponseModels;
using MovieAPI.Repositories;
using MovieAPI.Repositories.Interfaces;
using MovieAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.Services
{
    public class ProducerService : IProducerService
    {
        private readonly IProducerRepository _producerRepository;

        public ProducerService(IProducerRepository producerRepository)
        {
            _producerRepository = producerRepository;
        }

        public async Task<int> Add(ProducerRequestModel request)
        {
            var producer = new Producer()
            {
                Name = request.Name,
                Bio = request.Bio,
                DateOfBirth = request.DateOfBirth,
                Gender = request.Gender,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            Validate(producer);

            producer.Id=await _producerRepository.Add(producer);

            return producer.Id;
        }

        public async Task Delete(int id)
        {
            if (id < 1)
            {
                throw new BadRequestException("Invalid producer");
            }

            var producer = await _producerRepository.Get(id);

            if (producer == null)
            {
                throw new NotFoundException("Producer not found");
            }

            await _producerRepository.Delete(producer);

        }

        public async Task<List<ProducerResponseModel>> Get()
        {
            var producers= await _producerRepository.Get();
            return producers.Select(x => new ProducerResponseModel()
            {
                Id = x.Id,
                Name=x.Name,
                DateOfBirth=x.DateOfBirth,
                Gender=x.Gender,
                Bio=x.Bio,
            }).ToList();
        }

        public  async Task<ProducerResponseModel> Get(int id)
        {
            if (id < 1)
            {
                throw new BadRequestException("Invalid producer");
            }

            var producer = await _producerRepository.Get(id);

            if (producer == null)
            {
                throw new NotFoundException("producer not found");
            }

            var response= new ProducerResponseModel()
            {
                Id = producer.Id,
                Name = producer.Name,
                DateOfBirth = producer.DateOfBirth,
                Bio = producer.Bio,
                Gender = producer.Gender,
            };

            return response;
        }

        public async Task Update(int id, ProducerUpdateRequest request)
        {
           
            var existingProducer = await _producerRepository.Get(id);

            if (existingProducer == null)
            {
                throw new NotFoundException("Producer you are trying to update does not exists");
            }

            var producer = new Producer()
            {
                Id = id,
                Name = request.Name,
                DateOfBirth = (DateTime)request.DateOfBirth,
                Bio = request.Bio,
                Gender = request.Gender,
                UpdatedAt = DateTime.UtcNow,
                CreatedAt=existingProducer.CreatedAt
            };

            Validate(producer);

            await _producerRepository.Update(producer);
        }

        public void Validate(Producer request)
        {
            if (string.IsNullOrWhiteSpace(request.Name)|| int.TryParse(request.Name, out var resultName))
            {
                throw new BadRequestException("Enter proper Producer name");
            }

            if (string.IsNullOrWhiteSpace(request.Gender) || int.TryParse(request.Gender, out var resultGender))
            {
                throw new BadRequestException("Enter proper Producer gender");
            }
            if (string.IsNullOrWhiteSpace(request.Bio))
            {
                throw new BadRequestException("Enter proper Producer bio");
            }
            if (request.DateOfBirth == default)
            {
                throw new BadRequestException("Enter correct Producer Date");
            }


        }
    }
}
