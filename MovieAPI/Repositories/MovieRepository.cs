using Microsoft.Extensions.Configuration;
using MovieAPI.Models.DTOs;
using MovieAPI.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieAPI.Repositories
{
	
    public class MovieRepository : BaseRepository<MovieDTO>, IMoviesRepository
    {
        public MovieRepository(IConfiguration configuration)
            : base(configuration)
        {
        }
        public async Task<int> Add(MovieDTO movie)
        {

            return await base.ExecuteSPAsync<int>("usp_AddMovie", new
            {
                Name = movie.Name,
                YearOfRelease = movie.YearOfRelease,
                Plot = movie.Plot,
                Poster = movie.ImageUrl,
                ProducerId = movie.ProducerId,
                ActorIds = movie.ActorIds,
                GenreIds = movie.GenreIds
            });
        } 

        public async Task Delete(int id)
        {

			await base.ExecuteSPAsync<MovieDTO>("usp_DeleteMovie", new { MovieId = id });
        }

        public async Task<IEnumerable<MovieDTO>> Get()
        {
            const string query = @"
SELECT m.Id
	,m.Name
	,m.YearOfRelease
	,m.Plot
	,m.ProducerId
	,m.Poster AS [ImageUrl]
	,STRING_AGG(am.ActorId, ',') AS [ActorIds]
	,STRING_AGG(gm.GenreId, ',') AS [GenreIds]
FROM Movies m   WITH (NOLOCK)
INNER JOIN Actors_Movies am ON am.MovieId = m.Id
INNER JOIN Genres_Movies gm ON gm.MovieId = m.Id
GROUP BY m.Id
	,m.Name
	,m.YearOfRelease
	,m.Plot
	,m.ProducerId
	,m.Poster;";

            return await base.QueryAsync<MovieDTO>(query);
        }

        public async Task<MovieDTO> Get(int id)
        {
            const string query = @"
SELECT m.Id
	,m.Name
	,m.YearOfRelease
	,m.Plot
	,m.ProducerId
	,m.Poster AS [ImageUrl]
	,STRING_AGG(am.ActorId, ',') AS [ActorIds]
	,STRING_AGG(gm.GenreId, ',') AS [GenreIds]
FROM Movies m WITH (NOLOCK)
INNER JOIN Actors_Movies am ON am.MovieId = m.Id
INNER JOIN Genres_Movies gm ON gm.MovieId = m.Id
WHERE m.Id = @Id
GROUP BY m.Id
	,m.Name
	,m.YearOfRelease
	,m.Plot
	,m.ProducerId
	,m.Poster;
";

            return await base.QueryFirstOrDefaultAsync<MovieDTO>(query, new {Id=id});
        }

        public async Task Update(MovieDTO movie)
        {
            
            await base.ExecuteSPAsync<MovieDTO>("usp_UpdateMovie", new 
            {
                Id=movie.Id,
                Name=movie.Name,
                YearOfRelease=movie.YearOfRelease,
                Plot=movie.Plot,
                ProducerId=movie.ProducerId,
                Poster=movie.ImageUrl,
                ActorIds= movie.ActorIds,
                GenreIds = movie.GenreIds
            });
        }
    }
}

