using Microsoft.Extensions.Configuration;
using MovieAPI.Models.DatabaseModels;
using MovieAPI.Models.ResponseModels;
using MovieAPI.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieAPI.Repositories
{
    public class GenreRepository : BaseRepository<Genre>,IGenreRepository
    {
        public GenreRepository(IConfiguration configuration)
            :base(configuration)
        {
        }

        public async Task<IEnumerable<GenreResponseModel>> Get(List<int> ids)
        {
            const string query = @"
SELECT g.Id
	,g.Name
FROM Genres g WITH (NOLOCK)
WHERE g.Id IN @Ids;";

            return await base.QueryAsync<GenreResponseModel>(query,new {Ids= ids});
        }
        public async Task<int> Add(Genre genre)
        {
            const string query = @"
    INSERT INTO Genres (
	Name
	,CreatedAt
	,UpdatedAt
	)
OUTPUT INSERTED.Id
VALUES (
	@Name
	,@CreatedAt
	,@UpdatedAt
	)";
            return await base.ExecuteScalarAsync<int>(query, genre); 
        }

        public async Task Delete(Genre genre)
        {
            const string query = @"
DELETE
FROM Genres
WHERE Id = @Id

DELETE
FROM Genres_Movies
WHERE GenreId = @Id";

            await base.ExecuteAsync(query, new { Id = genre.Id });
        }

        public async Task<IEnumerable<Genre>> Get()
        {
            const string query = @"
SELECT [Id]
	,[Name]
    ,[CreatedAt]
    ,[UpdatedAt]
FROM [Genres](NOLOCK) ";

            return await base.QueryAsync<Genre>(query);
        }

        public async Task<Genre> Get(int id)
        {
            const string query = @"
SELECT [Id]
	,[Name]
    ,[CreatedAt]
    ,[UpdatedAt]
FROM [Genres](NOLOCK) 
WHERE Id=@Id";

            return await base.QueryFirstOrDefaultAsync<Genre>(query, new { Id = id });
        }

        public async Task Update(Genre genre)
        {
            const string query = @"
UPDATE Genres
SET Name = @Name
	,UpdatedAt = @UpdatedAt
WHERE Id = @Id";

            await base.ExecuteAsync(query, genre);
        

    }
    }
}
