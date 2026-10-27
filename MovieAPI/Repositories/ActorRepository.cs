using MovieAPI.Models.DatabaseModels;
using MovieAPI.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MovieAPI.Models.ResponseModels;

namespace MovieAPI.Repositories
{
    public class ActorRepository : BaseRepository<Actor>,IActorsRepository
    {

        public ActorRepository(IConfiguration configuration)
            : base(configuration)
        {
        }

        public async Task<IEnumerable<ActorResponseModel>> Get(List<int> ids)
        {
            const string query = @"
SELECT a.Id
	,a.Name
	,a.DateOfBirth
	,a.Gender
	,a.Bio
FROM Actors a WITH (NOLOCK)
WHERE a.Id IN @Ids;";

            return await base.QueryAsync<ActorResponseModel>(query, new {Ids=ids});

        }

        public async Task<int> Add(Actor actor)
        {
            const string query= @"
    INSERT INTO Actors (
	Name
	,Bio
	,DateOfBirth
	,Gender
	,CreatedAt
	,UpdatedAt
	)
OUTPUT INSERTED.Id
VALUES (
	@Name
	,@Bio
	,@DateOfBirth
	,@Gender
	,@CreatedAt
	,@UpdatedAt
	)";
            return await base.ExecuteScalarAsync<int>(query, actor);

        }

        public async Task Delete(Actor actor)
        {
            const string query = @"
DELETE
FROM Actors
WHERE Id = @Id

DELETE
FROM Actors_Movies
WHERE ActorId = @Id "; 

            await base.ExecuteAsync(query, new { Id = actor.Id });

        }

        public async Task<IEnumerable<Actor>> Get()
        {
            const string query = @"
SELECT [Id]
	,[Name]
	,[Bio]
	,[DateOfBirth]
	,[Gender]
    ,[CreatedAt]
    ,[UpdatedAt]
FROM [Actors](NOLOCK) ";
            
            return await base.QueryAsync<Actor>(query);
        }

        public async Task<Actor> Get(int id)
        {
            const string query= @"
SELECT [Id]
	,[Name]
	,[Bio]
	,[DateOfBirth]
	,[Gender]
    ,[CreatedAt]
    ,[UpdatedAt]
FROM [Actors](NOLOCK) 
WHERE Id=@Id";
            return await base.QueryFirstOrDefaultAsync<Actor>(query, new { Id=id });
        }

        public async Task Update(Actor actor)
        {
            const string query = @"
UPDATE Actors
SET Name = @Name
	,Bio = @Bio
	,DateOfBirth = @DateOfBirth
	,Gender = @Gender
	,UpdatedAt = @UpdatedAt
WHERE Id = @Id";

            await base.ExecuteAsync(query, actor);
        }

    }
}
