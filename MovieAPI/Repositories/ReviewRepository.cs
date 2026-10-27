using Microsoft.Extensions.Configuration;
using MovieAPI.Models.DatabaseModels;
using MovieAPI.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieAPI.Repositories
{
    public class ReviewRepository : BaseRepository<Review>, IReviewRepository
    {

        public ReviewRepository(IConfiguration configuration)
            : base(configuration)
        {
        }
        public async Task<int> Add(Review review)
        {
            const string query = @"
    INSERT INTO Reviews (
	Message
	,MovieId
	,CreatedAt
	,UpdatedAt
	)
OUTPUT INSERTED.Id
VALUES (
	@Message
	,@MovieId
	,@CreatedAt
	,@UpdatedAt
	)";
            return await base.ExecuteScalarAsync<int>(query, review);
        }

        public async Task Delete(Review review)
        {
            const string query = @"
DELETE
FROM Reviews
WHERE Id = @Id";

            await base.ExecuteAsync(query, new { Id = review.Id });
        }


        public async Task<IEnumerable<Review>> Get(int movieId)
        {
            const string query = @"
SELECT [Id]
	,[Message]
    ,[CreatedAt]
    ,[UpdatedAt]
FROM [Reviews](NOLOCK) 
WHERE MovieId=@MovieId";

            return await base.QueryAsync<Review>(query, new { MovieId = movieId });
        }

        public async Task<Review> Get(int id, int movieId)
        {
            const string query = @"
SELECT [Id]
	,[Message]
    ,[CreatedAt]
    ,[UpdatedAt]
FROM [Reviews](NOLOCK)
WHERE Id = @Id
	AND MovieId = @MovieId";
            return await base.QueryFirstOrDefaultAsync<Review>(query, new { Id = id, MovieId = movieId });
        }

        public async Task Update(Review review)
        {
            const string query = @"
UPDATE Reviews
SET Message = @Message
	,UpdatedAt = @UpdatedAt
WHERE Id = @Id";

            await base.ExecuteAsync(query, review);
        }
    }
}

