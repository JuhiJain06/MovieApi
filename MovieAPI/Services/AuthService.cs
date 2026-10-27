using Microsoft.AspNetCore.Identity;
using MovieAPI.Exceptions;
using MovieAPI.Helpers.Interface;
using MovieAPI.Models.DatabaseModels;
using MovieAPI.Models.RequestModels;
using MovieAPI.Repositories.Interfaces;
using MovieAPI.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace MovieAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _userRepository;
        private readonly IJwtTokenGenerator _token;


        public AuthService(IAuthRepository userRepository,IJwtTokenGenerator token)
        {
            _userRepository = userRepository;
            _token = token;
        }
        public async Task<string> Login(LoginRequest loginRequest)
        {
            var user = await _userRepository.Get(loginRequest.Email);

            if (user == null)
            {
                throw new UnauthorizedException("Invalid Email or Password");
            }

            var hasher = new PasswordHasher<User>();

            var result = hasher.VerifyHashedPassword(user, user.Password, loginRequest.Password);

            if(result==PasswordVerificationResult.Failed)
            {
                throw new UnauthorizedException("Invalid Email or Password");
            }

            
            return _token.Token(user);
            
        }

        public async Task SignUp(SignUpRequest signUp)
        {
            var hasher = new PasswordHasher<User>();

            var user = new User()
            {
                Name=signUp.Name,
                Email=signUp.Email,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            user.Password = hasher.HashPassword(user, signUp.Password);


            var alreadyExists = await _userRepository.Get(user.Email);

            if (alreadyExists != null)
            {
                throw new BadRequestException("User already exists");
            }

            Validate(user);

            await _userRepository.Add(user);

           
        }

        void Validate(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Name) || int.TryParse(user.Name, out var resultName))
            {
                throw new BadRequestException("Enter correct  User Name");
            }

            if  (string.IsNullOrWhiteSpace(user.Email))
            {
                throw new BadRequestException("Enter correct user email");
            }

             if (string.IsNullOrWhiteSpace (user.Password))
            {
                throw new BadRequestException("Enter correct user Password");
            }

             
        }
    }
}
