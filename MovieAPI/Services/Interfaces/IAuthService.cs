using MovieAPI.Models.RequestModels;
using System;
using System.Threading.Tasks;

namespace MovieAPI.Services.Interfaces
{
    public interface IAuthService
    {
        Task SignUp(SignUpRequest signUp);
        Task<string> Login(LoginRequest loginRequest);
    }
}
