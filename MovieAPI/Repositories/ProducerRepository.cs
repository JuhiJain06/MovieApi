using Microsoft.Extensions.Configuration;
using MovieAPI.Models.DatabaseModels;
using MovieAPI.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieAPI.Repositories
{
    public class ProducerRepository :BaseRepository<Producer>, IProducerRepository
    {

        public ProducerRepository(IConfiguration configuration)
            :base(configuration)
        {

        }
        public async Task<int> Add(Producer producer)
        {
            const string query = @"
    INSERT INTO Producers (
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
            return await base.ExecuteScalarAsync<int>(query, producer);
        }

        public async Task Delete(Producer producer)
        {
            const string query = @"
DELETE 
FROM Producers
Where Id = @Id";

            await base.ExecuteAsync(query, new { Id = producer.Id });

        }

        public async Task<IEnumerable<Producer>> Get()
        {
            const string query = @"
SELECT [Id]
	,[Name]
	,[Bio]
	,[DateOfBirth]
	,[Gender]
    ,[CreatedAt]
    ,[UpdatedAt]
FROM [Producers](NOLOCK) ";

            return await base.QueryAsync<Producer>(query);
        }

        public async Task<Producer> Get(int id)
        {
            const string query = @"
SELECT [Id]
	,[Name]
	,[Bio]
	,[DateOfBirth]
	,[Gender]
    ,[CreatedAt]
    ,[UpdatedAt]
FROM [Producers](NOLOCK) 
WHERE Id=@Id";
            return await base.QueryFirstOrDefaultAsync<Producer>(query, new { Id = id });
        }

        public async Task Update(Producer producer)
        {
            const string query = @"
UPDATE Producers
SET Name = @Name
	,Bio = @Bio
	,DateOfBirth = @DateOfBirth
	,Gender = @Gender
	,UpdatedAt = @UpdatedAt
WHERE Id = @Id";

            await base.ExecuteAsync(query, producer);
        }
    }
}
