using Dapper;
using Microsoft.VisualBasic;
using MovieAPI.Exceptions;
using MovieAPI.Models.DatabaseModels;
using MovieAPI.Models.DTOs;
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
    public class MovieService : IMovieService
    {
        private readonly IMoviesRepository _moviesRepository;
        private readonly IActorService _actorService;
        private readonly IGenreService _genreService;
        private readonly IProducerService _producerService;
        private readonly ISupabaseService _supabase;

        public MovieService(ISupabaseService supabase,IMoviesRepository movieRepository, IActorService actorService, IGenreService genreService,IProducerService producerService)
        {
            _moviesRepository = movieRepository;
            _actorService = actorService;
            _genreService = genreService;
            _producerService = producerService;
            _supabase = supabase;
        }

        public async Task<MovieResponseModel> MapToResponse(MovieDTO movie)
        {
            var actorEntity =await _actorService.Get(movie.ActorIds);
            var genreEntity = await _genreService.Get(movie.GenreIds);
            var producer= await _producerService.Get(movie.ProducerId);

            return new MovieResponseModel()
            {
                Id = movie.Id,
                Name = movie.Name,
                CoverImage = movie.ImageUrl,
                Plot = movie.Plot,
                YearOfRelease = movie.YearOfRelease,
                Producer = producer == null ? null : producer,
                Actors=actorEntity.ToList(),
                Genres=genreEntity.ToList(),
            };
        }

        public async Task<int> Add(MovieRequestModel request)
        {
            var movie = new MovieDTO()
            {
                Name = request.Name,
                YearOfRelease = request.YearOfRelease,
                Plot= request.Plot,
                ImageUrl= await _supabase.UploadPosterAsync(request.CoverImage,request.Name,request.YearOfRelease),
                ProducerId= request.ProducerId,
                ActorIds=string.Join(",",request.ActorsId),
                GenreIds= string.Join(",", request.GenresId),
            };

            Validate(movie);

            movie.Id=await _moviesRepository.Add(movie);

            return movie.Id;
        }

        public async Task Delete(int id)
        {
            if (id < 1)
            {
                throw new BadRequestException("Invalid movie");
            }

            var movie = await _moviesRepository.Get(id);

            if (movie == null)
            {
                throw new NotFoundException("movie not found");
            }

            await _moviesRepository.Delete(id);

        }

        public async Task<List<MovieResponseModel>> Get()
        {
            var movies= await _moviesRepository.Get();

            var response =await Task.WhenAll(movies.Select(x => MapToResponse(x)));

            return response.ToList();
            
        }

        public async Task<MovieResponseModel> Get(int id)
        {
            if (id < 1)
            {
                throw new BadRequestException("Invalid movie");
            }

            var movie = await _moviesRepository.Get(id);

            if (movie == null)
            {
                throw new NotFoundException("Movie not found");
            }


            return await MapToResponse(movie);
        }

        public async Task Update(int id, MovieUpdateRequest request)
        {
            
            var existingMovie = await _moviesRepository.Get(id);

            if (existingMovie == null)
            {
                throw new NotFoundException("Movie you are trying to update does not exists");
            }

            var movie = new MovieDTO()
            {
                Id = id,
                Name = request.Name,
                YearOfRelease = request.YearOfRelease,
                Plot = request.Plot,
                ImageUrl = await _supabase.UploadPosterAsync(request.CoverImage,request.Name,request.YearOfRelease),
                ProducerId = request.ProducerId,
                ActorIds= string.Join(",", request.ActorsId),
                GenreIds= string.Join(",", request.GenresId),
            };

            Validate(movie);

            await _moviesRepository.Update(movie);


        }

        public void Validate(MovieDTO request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new BadRequestException("Enter proper Movie name");
            }

             if (string.IsNullOrWhiteSpace(request.Plot))
            {
                throw new BadRequestException("Enter proper Movie plot");
            }
            if (string.IsNullOrWhiteSpace(request.ImageUrl))
            {
                throw new BadRequestException("Enter proper Movie coverimage");
            }
            if (request.YearOfRelease < 1888 || request.YearOfRelease > DateTime.Now.Year)
            {
                throw new BadRequestException("Please enter a valid year of release for the Movie.");
            }


        }
    }
}
