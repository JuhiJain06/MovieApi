using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MovieAPI.Models.DatabaseModels;
using MovieAPI.Models.DTOs;
using MovieAPI.Repositories.Interfaces;
using System.Threading.Tasks;

namespace MovieAPI.Repositories
{
    public class AuthRepository : BaseRepository<User>,IAuthRepository
    {

        public AuthRepository(IConfiguration configuration)
            :base(configuration)
        {
        }
        public async Task<User> Get(string email)
        {
            const string query = @"
SELECT [Id]
	,[Name]
	,[Email]
	,[Password]
FROM [Users](NOLOCK)
WHERE Email=@Email";

            return await base.QueryFirstOrDefaultAsync<User>(query,new {Email=email});
        }

        public async Task Add(User user)
        {
            const string query = @"
    INSERT INTO Users (
	Name
	,Email
	,Password
	,CreatedAt
	,UpdatedAt
	)
    VALUES (
	@Name
	,@Email
	,@Password
	,@CreatedAt
	,@UpdatedAt
	)";
            await base.ExecuteAsync(query,user);
        }
    }
}
