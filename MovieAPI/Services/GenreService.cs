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
    public class GenreService : IGenreService

    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<int> Add(GenreRequestModel request)
        {
            var genre = new Genre()
            {
                
                Name = request.Name,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            Validate(genre);

            genre.Id=await _genreRepository.Add(genre);

            return genre.Id;
        }

        public async Task Delete(int id)
        {
            if (id < 1)
            {
                throw new BadRequestException("Invalid genre");
            }

            var genre = await _genreRepository.Get(id);

            if (genre == null)
            {
                throw new NotFoundException("genre not found");
            }

            await _genreRepository.Delete(genre);
        }

        public async Task<List<GenreResponseModel>> Get()
        {
            var genres= await _genreRepository.Get();
            return genres.Select(x => new GenreResponseModel()
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
        }

        public async Task<GenreResponseModel> Get(int id)
        {
            if (id < 1)
            {
                throw new BadRequestException("Invalid genre");
            }

            var genre = await _genreRepository.Get(id);

            if (genre == null)
            {
                throw new NotFoundException("genre not found");
            }

            var response = new GenreResponseModel()
            {
                Id=genre.Id,
                Name= genre.Name,
            };
            return response;
        }

        public async Task<IEnumerable<GenreResponseModel>> Get(string ids)
        {
            var result = ids.Split(',').Where(x => int.TryParse(x, out _)).Select(int.Parse).Distinct().ToList();

            return await _genreRepository.Get(result);
        }

        public async Task Update(int id, GenreUpdateRequest request)
        {
            
            var existingGenre = await _genreRepository.Get(id);

            if (existingGenre == null)
            {
                throw new NotFoundException("Genre you are trying to update does not exists");
            }

            var genre = new Genre()
            {
                Id = id,
                Name = request.Name,
                UpdatedAt= DateTime.UtcNow,
                CreatedAt=existingGenre.CreatedAt
            };

            Validate(genre);

            genre.UpdatedAt = DateTime.UtcNow;

            await _genreRepository.Update(genre);
        }

        public void Validate(Genre request)
        {
            if (string.IsNullOrWhiteSpace(request.Name) || int.TryParse(request.Name, out var result) )
            {
                throw new BadRequestException("Enter proper Genre name");
            }
        }
    }
}
