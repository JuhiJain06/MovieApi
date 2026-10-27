using MovieAPI.Exceptions;
using MovieAPI.Models.DatabaseModels;
using MovieAPI.Models.RequestModels;
using MovieAPI.Models.ResponseModels;
using MovieAPI.Repositories.Interfaces;
using MovieAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.Services
{
    public class ActorService : IActorService
    {
        private readonly IActorsRepository _actorsRepository;
        public ActorService(IActorsRepository actorRepository)
        {
            _actorsRepository = actorRepository;
        }

        public async Task<int> Add(ActorRequestModel request)
        {
            var actor = new Actor()
            {
                Name = request.Name,
                Bio = request.Bio,
                DateOfBirth = request.DateOfBirth,
                Gender = request.Gender,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            Validate(actor);

            actor.Id= await _actorsRepository.Add(actor);
            return actor.Id;
        }

        public async Task Delete(int id)
        {
            if (id < 1)
            {
                throw new BadRequestException("Invalid actor");
            }

            var actor = await _actorsRepository.Get(id);

            if (actor == null)
            {
                throw new NotFoundException("Actor not found");
            }

            await _actorsRepository.Delete(actor);
        }

        public async Task<List<ActorResponseModel>> Get()
        {
            var actors= await _actorsRepository.Get();
            return actors.Select(x => new ActorResponseModel()
            {
                Id = x.Id,
                Name = x.Name,
                DateOfBirth = x.DateOfBirth,
                Bio = x.Bio,
                Gender = x.Gender,
            }).ToList();
        }

        public async Task<ActorResponseModel> Get(int id)
        {
            if (id < 1)
            {
                throw new BadRequestException("Invalid actor");
            }

            var actor = await _actorsRepository.Get(id);

            if (actor == null)
            {
                throw new NotFoundException("Actor not found");
            }

            var response = new ActorResponseModel()
            {
                Id = actor.Id,
                Name = actor.Name,
                DateOfBirth = actor.DateOfBirth,
                Bio = actor.Bio,
                Gender = actor.Gender,

            };

            return response;
        }

        public async Task<IEnumerable<ActorResponseModel>> Get(string ids)
        {
            var result=ids.Split( ',' ).Where(x=>int.TryParse(x,out _)).Select(int.Parse).Distinct().ToList();

            return await _actorsRepository.Get(result);
        }

        public async Task Update(int id, ActorUpdateRequest request)
        {
            
            var existingActor = await _actorsRepository.Get(id);

            if (existingActor == null)
            {
                throw new NotFoundException("Actor you are trying to update does not exists");
            }
            var actor = new Actor()
            {
                Id = id,
                Name = request.Name,
                DateOfBirth = request.DateOfBirth,
                Bio = request.Bio,
                Gender = request.Gender,
                UpdatedAt = DateTime.UtcNow,
                CreatedAt=existingActor.CreatedAt
            };

            Validate(actor);

            await _actorsRepository.Update(actor);
        }

        public void Validate(Actor request)
        {
            if (string.IsNullOrWhiteSpace(request.Name) || int.TryParse(request.Name,out var resultName))
            {
                throw new BadRequestException("Enter proper Actor name");
            }

            if ( string.IsNullOrWhiteSpace(request.Gender)|| int.TryParse(request.Gender, out var resultGender) )
            {
                throw new BadRequestException("Enter proper Actor gender");
            }
            
            if (request.DateOfBirth == default || request.DateOfBirth > DateTime.Today)
            {
                throw new BadRequestException("Enter correct Actor Date");

            }
            if (string.IsNullOrWhiteSpace(request.Bio))
            {
                throw new BadRequestException("Enter correct Actor bio");
            }



        }


    }
}
