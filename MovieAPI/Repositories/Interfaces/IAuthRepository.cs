using Microsoft.AspNetCore.Identity;
using MovieAPI.Models.DatabaseModels;
using MovieAPI.Models.RequestModels;
using System.Threading.Tasks;
using static MovieAPI.Models.RequestModels.LoginRequest;

namespace MovieAPI.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> Get(string email);
        Task Add(User user);
    }
}